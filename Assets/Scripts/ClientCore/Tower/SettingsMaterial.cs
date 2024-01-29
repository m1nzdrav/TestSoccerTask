using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Fire
{
    public class SettingsMaterial : NetworkBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private List<Material> materials;

        [ClientRpc]
        public void ChangeMaterialClientRpc(int idMaterial)
        {
            meshRenderer.material = materials[idMaterial];
        }
    }
}