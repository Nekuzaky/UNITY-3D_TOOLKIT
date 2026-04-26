using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Reads inputs from the New Input System and exposes the values.
    /// Requires an InputActionAsset assigned in the inspector.
    /// All input logic lives here, not in PlayerController.
    /// </summary>
    public sealed class InputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actionAsset;
        [SerializeField] private string _moveActionName = "Move";
        [SerializeField] private string _jumpActionName = "Jump";
        [SerializeField] private string _sprintActionName = "Sprint";
        [SerializeField] private string _attackActionName = "Attack";
        [SerializeField] private string _interactActionName = "Interact";
        [SerializeField] private string _dashActionName = "Dash";
        [SerializeField] private string _pauseActionName = "Pause";

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private InputAction _attackAction;
        private InputAction _interactAction;
        private InputAction _dashAction;
        private InputAction _pauseAction;

        public Vector2 MoveAxis { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool JumpHeld { get; private set; }
        public bool SprintHeld { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool DashPressed { get; private set; }
        public bool PausePressed { get; private set; }

        private void Awake()
        {
            Debug.Assert(_actionAsset != null, "[InputHandler] _actionAsset is null"); // R5
            if (_actionAsset == null) return;
            _moveAction = _actionAsset.FindAction(_moveActionName, true);
            _jumpAction = _actionAsset.FindAction(_jumpActionName, true);
            _sprintAction = _actionAsset.FindAction(_sprintActionName, false);
            _attackAction = _actionAsset.FindAction(_attackActionName, false);
            _interactAction = _actionAsset.FindAction(_interactActionName, false);
            _dashAction = _actionAsset.FindAction(_dashActionName, false);
            _pauseAction = _actionAsset.FindAction(_pauseActionName, false);
        }

        private void OnEnable() { _actionAsset?.Enable(); }
        private void OnDisable() { _actionAsset?.Disable(); }

        private void Update()
        {
            MoveAxis = _moveAction != null ? _moveAction.ReadValue<Vector2>() : Vector2.zero;
            JumpPressed = _jumpAction != null && _jumpAction.WasPressedThisFrame();
            JumpHeld = _jumpAction != null && _jumpAction.IsPressed();
            SprintHeld = _sprintAction != null && _sprintAction.IsPressed();
            AttackPressed = _attackAction != null && _attackAction.WasPressedThisFrame();
            InteractPressed = _interactAction != null && _interactAction.WasPressedThisFrame();
            DashPressed = _dashAction != null && _dashAction.WasPressedThisFrame();
            PausePressed = _pauseAction != null && _pauseAction.WasPressedThisFrame();
        }

        public void EnableInput() { _actionAsset?.Enable(); }
        public void DisableInput() { _actionAsset?.Disable(); }
    }
}
