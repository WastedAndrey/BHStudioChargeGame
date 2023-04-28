using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public class DefaultInput : MonoBehaviour, IPlayerInput
    {
        public Vector3 MovementInput { get => _movementInput; }
        public Vector2 RotationInput { get => _rotationInput; }
        public bool FireInput { get => _fireInput; }

        private Vector3 _movementInput;
        private Vector2 _rotationInput;
        private bool _fireInput;



        private void Update()
        {
            _movementInput.x = Input.GetAxis("Horizontal");
            _movementInput.z = Input.GetAxis("Vertical");
            _rotationInput.x = Input.GetAxis("Mouse X");
            _rotationInput.y = Input.GetAxis("Mouse Y");
            _fireInput = Input.GetMouseButton(0);
        }
    }
}