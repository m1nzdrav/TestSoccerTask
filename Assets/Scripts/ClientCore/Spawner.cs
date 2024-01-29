using System.Collections.Generic;
using ClientCore.Data;
using ClientCore.Fire;
using ServerCore;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace ClientCore
{
    public class Spawner : NetworkBehaviour
    {
        [SerializeField] private GameObject towerPrefab, gatePrefab;
        [SerializeField] private List<Transform> spawnPoint;
        [SerializeField] private List<Transform> gatesSpawnPoint;

        private void Start()
        {
            StartServer();
        }

        private void StartServer()
        {
            if (!NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsServer) return;


            foreach (NetworkClient VARIABLE in NetworkManager.Singleton.ConnectedClients.Values)
            {
                int playerTeam = VARIABLE.PlayerObject.GetComponent<PlayerData>().GetVariable();
                SpawnTower(VARIABLE, playerTeam);
                SpawnGate(VARIABLE, playerTeam);
            }
        }

        private void SpawnGate(NetworkClient VARIABLE, int playerTeam)
        {
            NetworkObject gate = NetworkObjectPool.Singleton.GetNetworkObject(gatePrefab,
                GetGateSpawnPoint(playerTeam).position, GetGateSpawnPoint(playerTeam).rotation);
            
            gate.SpawnWithOwnership(VARIABLE.ClientId);
            gate.GetComponent<GateData>().SetTeamClientRpc(playerTeam,GetGateSpawnPoint(playerTeam).rotation);
            gate.GetComponent<SettingsMaterial>().ChangeMaterialClientRpc(playerTeam);
        }


        private void SpawnTower(NetworkClient VARIABLE, int playerTeam)
        {
            NetworkObject tower = NetworkObjectPool.Singleton.GetNetworkObject(towerPrefab,
                GetTowerSpawnPoint(playerTeam).position, quaternion.identity);

            tower.SpawnWithOwnership(VARIABLE.ClientId);
            tower.GetComponent<SettingsMaterial>().ChangeMaterialClientRpc(playerTeam);
        }


        private Transform GetTowerSpawnPoint(int count)
        {
            return spawnPoint[count];
        }

        private Transform GetGateSpawnPoint(int count)
        {
            return gatesSpawnPoint[count];
        }
    }
}