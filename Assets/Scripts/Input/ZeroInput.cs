using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public class ZeroInput : MonoBehaviour, IPlayerInput
    {
        public Vector3 MovementInput { get => Vector3.zero; }
        public Vector2 RotationInput { get => Vector2.zero; }
        public bool FireInput { get => false; }

    }
}