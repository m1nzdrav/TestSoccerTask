using Unity.Netcode;
using UnityEngine;

namespace ClientCore.Movement
{
    public class RotateTower : NetworkBehaviour
    {
        [SerializeField] Transform character;
        [SerializeField] private float sensitivity = 2;
        [SerializeField] private float smoothing = 1.5f;

        Vector2 velocity;
        Vector2 frameVelocity;


        void Start()
        {
            // Lock the mouse cursor to the game screen.
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (!IsOwner)
                return;
            
            Move();
        }

        private void Move()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            transform.localRotation = Quaternion.Euler(-velocity.y,velocity.x,transform.localRotation.z);
        }
    }
}