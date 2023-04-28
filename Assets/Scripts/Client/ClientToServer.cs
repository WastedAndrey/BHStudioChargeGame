using Mirror;
using UnityEngine;

namespace ChargeGame
{
    public class ClientToServer : NetworkBehaviour, IPlayerInput
    {
        [SerializeField]
        private ClientContext _clientContext;

        [SerializeField]
        [SyncVar]
        private float _horizontalInput;
        [SerializeField]
        [SyncVar]
        private float _verticalInput;
        [SerializeField]
        [SyncVar]
        private float _rotationInputX;
        [SerializeField]
        [SyncVar]
        private float _rotationInputY;
        [SerializeField]
        [SyncVar]
        private bool _fireInput;


        public Vector3 MovementInput { get => new Vector3(_horizontalInput, 0, _verticalInput); }
        public Vector2 RotationInput { get => new Vector2(_rotationInputX, _rotationInputY); }
        public bool FireInput { get => _fireInput; }


        private void Update()
        {
            if (isLocalPlayer)
            {
                _horizontalInput = Input.GetAxis("Horizontal");
                _verticalInput = Input.GetAxis("Vertical");
                _rotationInputX = Input.GetAxisRaw("Mouse X");
                _rotationInputY = Input.GetAxisRaw("Mouse Y");
                _fireInput = Input.GetMouseButton(0);
            }
        }
    }
}