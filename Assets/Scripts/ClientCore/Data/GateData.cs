using System;
using System.Collections.Generic;
using System.ComponentModel;
using UI;
using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Data
{
    [Serializable]
    struct MinMax
    {
        public int min;
        public int max;
    }

    public class GateData : NetworkBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private int team = 0;
        [SerializeField] private List<Vector3> direction;
        [SerializeField] private List<Vector3> scale;
        [SerializeField] private List<MinMax> minMaxes;
         private HashSet<ulong> _stockBall;

        private void Start()
        {
            _stockBall = new HashSet<ulong>(4);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            transform.position += direction[team];
            if (team < 2)
            {
                if (transform.position.x >= minMaxes[team].max || transform.position.x <= minMaxes[team].min)
                    direction[team] *= -1;
            }
            else if (transform.position.z >= minMaxes[team].max || transform.position.z <= minMaxes[team].min)
                direction[team] *= -1;
        }

        [ClientRpc]
        public void SetTeamClientRpc(int _team, Quaternion quaternion)
        {
            team = _team;
            transform.localScale = scale[team];
            transform.rotation = quaternion;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<BallData>(out BallData ballData) ||_stockBall.Contains(ballData.NetworkObject.NetworkObjectId)) return;

            try
            {
                _stockBall.Add(ballData.NetworkObject.NetworkObjectId);

                if (IsOwner) DespawnBallServerRPC(ballData.NetworkObject.NetworkObjectId, ballData.Team, team);
            }
            catch (Exception e)
            {
                Debug.LogError("try add second object");
            }
            
            


        }

        [ServerRpc]
        private void DespawnBallServerRPC(ulong networkObjectNetworkObjectId, int who, int to)
        {
            NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectNetworkObjectId,
                out NetworkObject networkObject);
            networkObject?.Despawn();
            UpdateUIClientRpc(who, to, networkObjectNetworkObjectId);
        }

        [ClientRpc]
        private void UpdateUIClientRpc(int who, int to, ulong networkObjectNetworkObjectId)
        {
            _stockBall.Remove(networkObjectNetworkObjectId);
            ScoreUI.Singleton.Gool(who, to);
        }
    }
}