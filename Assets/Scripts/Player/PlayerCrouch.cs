using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Player
{
    /// <summary>Crouch as CharacterController height adjustment.</summary>
    public sealed class PlayerCrouch : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [Min(0.1f)] [SerializeField] private float _standingHeight = 1.8f;
        [Min(0.1f)] [SerializeField] private float _crouchHeight = 1.0f;
        [Min(0f)] [SerializeField] private float _transitionSpeed = 8f;
        [SerializeField] private InputActionReference _crouchAction;

        public bool IsCrouching { get; private set; }
        private float _targetHeight;

        private void Awake()
        {
            if (_controller == null) _controller = GetComponent<CharacterController>();
            Debug.Assert(_controller != null, "[PlayerCrouch] _controller is null"); // R5
            _targetHeight = _standingHeight;
        }

        private void OnEnable() { if (_crouchAction != null) _crouchAction.action.Enable(); }
        private void OnDisable() { if (_crouchAction != null) _crouchAction.action.Disable(); }

        private void Update()
        {
            if (_crouchAction != null && _crouchAction.action.WasPressedThisFrame())
            {
                IsCrouching = !IsCrouching;
                _targetHeight = IsCrouching ? _crouchHeight : _standingHeight;
            }
            _controller.height = Mathf.Lerp(_controller.height, _targetHeight, _transitionSpeed * Time.deltaTime);
            _controller.center = new Vector3(0f, _controller.height * 0.5f, 0f);
        }
    }
}
