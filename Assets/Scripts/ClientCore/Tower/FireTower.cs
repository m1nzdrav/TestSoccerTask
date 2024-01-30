using System;
using System.Collections.Generic;
using ClientCore.Data;
using ServerCore;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Fire
{
    public class FireTower : NetworkBehaviour
    {
        [SerializeField] private Rigidbody ball;
        [SerializeField] private Transform gun;
        [SerializeField] private float force = 1000;
        [SerializeField] private float forceRadius = 1;


        private void Update()
        {
            if (!IsOwner)
                return;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                force += 1;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                FireServerRpc(OwnerClientId, force);
                force = 10;
            }
        }

        [ServerRpc]
        private void FireServerRpc(ulong ownerId, float _force)
        {
            NetworkManager.Singleton.ConnectedClients.TryGetValue(ownerId, out NetworkClient client);

            NetworkObject _ball =
                NetworkObjectPool.Singleton.GetNetworkObject(ball.gameObject, gun.position,
                    quaternion.identity);
            _ball.Spawn();
            _ball.GetComponent<BallData>()
                .SetTeamClientRpc(client.PlayerObject.GetComponent<PlayerData>().GetVariable());
            _ball.GetComponent<Rigidbody>().AddForce(gun.forward * _force,ForceMode.Impulse);
            _ball.GetComponent<SettingsMaterial>()
                .ChangeMaterialClientRpc(client.PlayerObject.GetComponent<PlayerData>().GetVariable());
        }
    }
}