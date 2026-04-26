using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// 3D Rigidbody-based player controller. Camera-relative movement.
    /// Jumps when grounded. Optional Sprint / Dash via PlayerSprint / PlayerDash.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController3D : PlayerControllerBase
    {
        [Header("Movement")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private LayerMask _groundMask = ~0;
        [SerializeField] private float _groundCheckDistance = 0.15f;
        [SerializeField] private float _airControl = 0.4f;

        private Rigidbody _rigidbody;
        private bool _isGrounded;

        public bool IsGrounded => _isGrounded;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
            if (_cameraTransform == null && Camera.main != null) _cameraTransform = Camera.main.transform;
            _rigidbody.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            if (!IsControlEnabled) return;
            UpdateGrounded();
            ApplyMovement();
            ApplyJump();
        }

        private void UpdateGrounded()
        {
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            _isGrounded = Physics.Raycast(origin, Vector3.down, 0.1f + _groundCheckDistance, _groundMask, QueryTriggerInteraction.Ignore);
        }

        private void ApplyMovement()
        {
            Vector2 axis = _input.MoveAxis;
            Vector3 fwd = _cameraTransform != null ? Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized : Vector3.forward;
            Vector3 right = _cameraTransform != null ? Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized : Vector3.right;
            Vector3 desired = (fwd * axis.y + right * axis.x);
            if (desired.sqrMagnitude > 1f) desired.Normalize();

            float targetSpeed = _stats.MoveSpeed * (_input.SprintHeld ? _stats.SprintMultiplier : 1f);
            Vector3 targetVelocity = desired * targetSpeed;

            float accel = _isGrounded ? _stats.Acceleration : _stats.Acceleration * _airControl;
            float decel = _isGrounded ? _stats.Deceleration : _stats.Deceleration * _airControl;
            float rate = desired.sqrMagnitude > 0.01f ? accel : decel;

            Vector3 horizontal = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
            horizontal = Vector3.MoveTowards(horizontal, targetVelocity, rate * Time.fixedDeltaTime);
            _rigidbody.linearVelocity = new Vector3(horizontal.x, _rigidbody.linearVelocity.y, horizontal.z);
            CurrentVelocity = _rigidbody.linearVelocity;
        }

        private void ApplyJump()
        {
            if (!_input.JumpPressed || !_isGrounded) return;
            var v = _rigidbody.linearVelocity;
            v.y = _stats.JumpForce;
            _rigidbody.linearVelocity = v;
        }

        public void Teleport(Vector3 worldPosition)
        {
            Debug.Assert(_rigidbody != null, "[PlayerController3D.Teleport] _rigidbody is null"); // R5
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.position = worldPosition;
            transform.position = worldPosition;
        }
    }
}
