using System;
using UnityEngine;

namespace ChargeGame
{
    [CreateAssetMenu(fileName = "CameraContext", menuName = "ScriptableObjects/CameraContext")]
    public class CameraContext : ScriptableObject
    {
        [SerializeField]
        private Vector3 _position;
        [SerializeField]
        private Quaternion _rotation;
        public Vector3 Position { get => _position; }
        public Quaternion Rotation { get => _rotation; }
        public Action Updated;

        public void SetValues(Vector3 position, Quaternion rotation)
        {
            _position = position;
            _rotation = rotation;
            Updated?.Invoke(); 
        }
    }
}
