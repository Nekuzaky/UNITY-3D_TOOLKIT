using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// 3D controller variant based on a CharacterController (kinematic).
    /// More deterministic, less reactive to forces; prefer when you want
    /// "snappy" platformer gameplay without complicated physics.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterController3D : PlayerControllerBase
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _gravity = -25f;
        [SerializeField] private float _groundedGravity = -2f;

        private CharacterController _controller;
        private Vector3 _verticalVelocity;
        private bool _isGrounded;

        public bool IsGrounded => _isGrounded;

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
            if (_cameraTransform == null && Camera.main != null) _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (!IsControlEnabled) { _verticalVelocity = Vector3.zero; return; }
            UpdateGrounded();
            ApplyVerticalForces();
            ApplyHorizontalMovement();
        }

        private void UpdateGrounded()
        {
            _isGrounded = _controller.isGrounded;
            if (_isGrounded && _verticalVelocity.y < 0f) _verticalVelocity.y = _groundedGravity;
        }

        private void ApplyVerticalForces()
        {
            if (_input.JumpPressed && _isGrounded)
            {
                _verticalVelocity.y = _stats.JumpForce;
            }
            _verticalVelocity.y += _gravity * Time.deltaTime;
        }

        private void ApplyHorizontalMovement()
        {
            Vector2 axis = _input.MoveAxis;
            Vector3 fwd = _cameraTransform != null ? Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized : Vector3.forward;
            Vector3 right = _cameraTransform != null ? Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized : Vector3.right;
            Vector3 desired = (fwd * axis.y + right * axis.x);
            if (desired.sqrMagnitude > 1f) desired.Normalize();

            float speed = _stats.MoveSpeed * (_input.SprintHeld ? _stats.SprintMultiplier : 1f);
            Vector3 motion = desired * speed + _verticalVelocity;
            _controller.Move(motion * Time.deltaTime);
            CurrentVelocity = motion;
        }
    }
}
