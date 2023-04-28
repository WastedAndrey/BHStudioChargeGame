using UnityEngine;

namespace ChargeGame
{

    [CreateAssetMenu(fileName = "ClientSettings", menuName = "ScriptableObjects/ClientSettings")]
    public class ClientSettings : ScriptableObject
    {
        [SerializeField]
        private Vector2 _cameraVerticalRotationDelta = new Vector2(10, 50);

        public Vector2 CameraVerticalRotationDelta { get => _cameraVerticalRotationDelta; }
    }
}