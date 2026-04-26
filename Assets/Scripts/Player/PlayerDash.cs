using System.Collections;
using UnityEngine;
using GameJamToolkit.Utils;

namespace GameJamToolkit.Player
{
    /// <summary>Directional dash based on current velocity. Cooldown + optional invuln.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerDash : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private InputHandler _input;
        [Min(0f)] [SerializeField] private float _dashDuration = 0.18f;
        [SerializeField] private bool _zeroGravityDuringDash = true;

        private Rigidbody _rigidbody;
        private Cooldown _cooldown;
        private bool _isDashing;

        public bool IsDashing => _isDashing;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_stats == null) _stats = GetComponent<PlayerStats>();
            if (_input == null) _input = GetComponent<InputHandler>();
            Debug.Assert(_stats != null, "[PlayerDash] _stats is null"); // R5
            Debug.Assert(_input != null, "[PlayerDash] _input is null"); // R5
            _cooldown = Cooldown.Of(Mathf.Max(0.01f, _stats != null ? _stats.DashCooldown : 1f));
        }

        private void Update()
        {
            if (!_input.DashPressed || _isDashing) return;
            if (!_cooldown.TryConsume()) return;
            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            _isDashing = true;
            Vector3 dir = _rigidbody.linearVelocity;
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.01f) dir = transform.forward;
            dir.Normalize();

            bool prevGravity = _rigidbody.useGravity;
            if (_zeroGravityDuringDash) _rigidbody.useGravity = false;

            float t = 0f;
            // R2: bounded by _dashDuration
            while (t < _dashDuration)
            {
                _rigidbody.linearVelocity = dir * _stats.DashForce + (_zeroGravityDuringDash ? Vector3.zero : Vector3.up * _rigidbody.linearVelocity.y);
                t += Time.deltaTime;
                yield return null;
            }

            _rigidbody.useGravity = prevGravity;
            _isDashing = false;
        }
    }
}
