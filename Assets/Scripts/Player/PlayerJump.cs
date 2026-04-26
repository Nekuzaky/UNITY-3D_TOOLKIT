using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Extended jump: double-jump, coyote time, jump buffer.
    /// Combine with a PlayerController3D / CharacterController3D.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(InputHandler))]
    public sealed class PlayerJump : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private InputHandler _input;
        [SerializeField] private LayerMask _groundMask = ~0;
        [Min(1)] [SerializeField] private int _maxJumps = 2;
        [Min(0f)] [SerializeField] private float _coyoteTime = 0.12f;
        [Min(0f)] [SerializeField] private float _bufferTime = 0.12f;

        private Rigidbody _rigidbody;
        private int _jumpsRemaining;
        private float _coyoteUntil;
        private float _bufferUntil;
        private bool _wasGrounded;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_stats == null) _stats = GetComponent<PlayerStats>();
            if (_input == null) _input = GetComponent<InputHandler>();
            Debug.Assert(_stats != null, "[PlayerJump] _stats is null"); // R5
            Debug.Assert(_input != null, "[PlayerJump] _input is null"); // R5
        }

        private void Update()
        {
            if (_input.JumpPressed) _bufferUntil = Time.time + _bufferTime;

            bool grounded = IsGrounded();
            if (grounded && !_wasGrounded) _jumpsRemaining = _maxJumps;
            if (grounded) _coyoteUntil = Time.time + _coyoteTime;
            _wasGrounded = grounded;

            if (Time.time > _bufferUntil) return;
            bool canCoyote = Time.time <= _coyoteUntil && _jumpsRemaining == _maxJumps;
            if (canCoyote || _jumpsRemaining > 0) DoJump();
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.15f, _groundMask, QueryTriggerInteraction.Ignore);
        }

        private void DoJump()
        {
            Vector3 v = _rigidbody.linearVelocity;
            v.y = _stats.JumpForce;
            _rigidbody.linearVelocity = v;
            _jumpsRemaining--;
            _bufferUntil = 0f;
            _coyoteUntil = 0f;
        }
    }
}
