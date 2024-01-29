using System;
using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Data
{
    public class BallData : NetworkBehaviour
    {
        [SerializeField] private int team;
       

        public int Team => team;

        [ClientRpc]
        public void SetTeamClientRpc(int team)
        {
            this.team = team;

        }
    }
}