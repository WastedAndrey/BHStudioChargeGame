using UnityEngine;

namespace ChargeGame
{
    public interface IPlayerInput
    {
        public Vector3 MovementInput { get; }
        public Vector2 RotationInput { get; }
        public bool FireInput { get; }

    }
}
