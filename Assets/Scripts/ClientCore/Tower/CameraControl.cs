using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Tower
{
    public class CameraControl : NetworkBehaviour
    {
        [SerializeField] private Camera camera;
        
        private void Start()
        {
            if (!IsOwner) camera.gameObject.SetActive(false);
        }
    }
}