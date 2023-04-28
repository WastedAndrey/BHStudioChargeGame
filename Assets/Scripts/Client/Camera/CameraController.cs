using Cinemachine;
using UnityEngine;

namespace ChargeGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private ClientContext _clientContext;
        [SerializeField]
        private CameraContext _cameraContext;
        [SerializeField]
        private Transform _cameraTransform;
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;
        [SerializeField]
        private CinemachineBrain _cinemachineBrain;

        private void OnEnable()
        {
            OnPlayerUnitAdded(_clientContext.PlayerUnit);
            _clientContext.PlayerUnitChanged += OnPlayerUnitAdded;
        }
        private void OnDisable ()
        {
            _clientContext.PlayerUnitChanged -= OnPlayerUnitAdded;
        }

        private void OnPlayerUnitAdded(ClientUnit unit)
        {
            if (unit != null)
                SetTarget(unit.CameraPoint);
        }
        
        public void SetTarget(Transform target)
        {
            _virtualCamera.m_Follow = target; 
        }

        private void LateUpdate()
        {
            _cinemachineBrain.ManualUpdate();
            _cameraContext.SetValues(_cameraTransform.position, _cameraTransform.rotation);
        }
    }
}
