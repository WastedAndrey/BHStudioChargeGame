using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    public class FaceToCamera : MonoBehaviour
    {
        [Header("Global links")]
        [SerializeField]
        private CameraContext _cameraContext;
        [Header("Component links")]
        [SerializeField]
        private Transform _transform;

        private void OnEnable()
        {
            _cameraContext.Updated += OnCameraUpdate;
        }

        private void OnDisable()
        {
            _cameraContext.Updated -= OnCameraUpdate;
        }

        private void OnCameraUpdate()
        {
            _transform.rotation = _cameraContext.Rotation;
        }
    }
}

