using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// First-person look: rotates player yaw, pitches a child camera.
    /// Use PlayerLook OR a separate third-person camera, not both.
    /// </summary>
    public sealed class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private float _sensitivity = 0.15f;
        [SerializeField] private float _minPitch = -85f;
        [SerializeField] private float _maxPitch = 85f;
        [SerializeField] private bool _lockCursor = true;

        private float _yaw;
        private float _pitch;

        private void Awake()
        {
            Debug.Assert(_cameraPivot != null, "[PlayerLook] _cameraPivot is null"); // R5
            Debug.Assert(_lookAction != null, "[PlayerLook] _lookAction is null"); // R5
            _yaw = transform.eulerAngles.y;
        }

        private void OnEnable()
        {
            if (_lookAction != null) _lookAction.action.Enable();
            if (_lockCursor) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        }

        private void OnDisable()
        {
            if (_lookAction != null) _lookAction.action.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            Vector2 delta = _lookAction != null ? _lookAction.action.ReadValue<Vector2>() : Vector2.zero;
            _yaw += delta.x * _sensitivity;
            _pitch -= delta.y * _sensitivity;
            _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
            transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
            if (_cameraPivot != null) _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }
    }
}
