using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Third-person orbit camera. Rotates around the target.</summary>
    public sealed class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _heightOffset = 1.6f;
        [SerializeField] private float _sensitivity = 0.2f;
        [SerializeField] private Vector2 _pitchClamp = new Vector2(-30f, 70f);
        [SerializeField] private float _smoothing = 12f;

        private float _yaw;
        private float _pitch = 20f;

        private void OnEnable() { if (_lookAction != null) _lookAction.action.Enable(); }
        private void OnDisable() { if (_lookAction != null) _lookAction.action.Disable(); }

        private void LateUpdate()
        {
            if (_target == null) return;
            Vector2 delta = _lookAction != null ? _lookAction.action.ReadValue<Vector2>() : Vector2.zero;
            _yaw += delta.x * _sensitivity;
            _pitch -= delta.y * _sensitivity;
            _pitch = Mathf.Clamp(_pitch, _pitchClamp.x, _pitchClamp.y);

            Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0f);
            Vector3 desired = _target.position + Vector3.up * _heightOffset + rot * (Vector3.back * _distance);
            transform.position = Vector3.Lerp(transform.position, desired, _smoothing * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((_target.position + Vector3.up * _heightOffset) - transform.position), _smoothing * Time.deltaTime);
        }

        public void SetTarget(Transform t) { _target = t; }
    }
}
