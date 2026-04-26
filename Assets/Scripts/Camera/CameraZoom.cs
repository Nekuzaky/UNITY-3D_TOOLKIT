using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Changes the camera FOV based on an InputAction (scroll wheel).</summary>
    [RequireComponent(typeof(Camera))]
    public sealed class CameraZoom : MonoBehaviour
    {
        [SerializeField] private InputActionReference _zoomAction;
        [SerializeField] private float _minFov = 30f;
        [SerializeField] private float _maxFov = 80f;
        [SerializeField] private float _zoomSpeed = 5f;
        [SerializeField] private float _smoothing = 8f;

        private Camera _camera;
        private float _targetFov;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            Debug.Assert(_camera != null, "[CameraZoom] _camera is null"); // R5
            _targetFov = _camera.fieldOfView;
        }

        private void OnEnable() { if (_zoomAction != null) _zoomAction.action.Enable(); }
        private void OnDisable() { if (_zoomAction != null) _zoomAction.action.Disable(); }

        private void Update()
        {
            float scroll = _zoomAction != null ? _zoomAction.action.ReadValue<float>() : 0f;
            _targetFov = Mathf.Clamp(_targetFov - scroll * _zoomSpeed, _minFov, _maxFov);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFov, _smoothing * Time.deltaTime);
        }
    }
}
