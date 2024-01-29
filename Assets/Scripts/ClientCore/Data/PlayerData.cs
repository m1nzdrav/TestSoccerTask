using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Data
{
    public class PlayerData : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<int> playerScore = new NetworkVariable<int>(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        [SerializeField] private NetworkVariable<int> playerColor = new NetworkVariable<int>(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);


        [ContextMenu("ChangeColor")]
        public void ChangeVariable(int value)
        {
            playerColor.Value = value;
        }

        public int GetVariable()
        {
            return playerColor.Value;
        }
    }
}