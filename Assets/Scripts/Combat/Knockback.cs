using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Receives knockback as Rigidbody. Attach to anything that should be
    /// knocked back by hitboxes.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Knockback : MonoBehaviour
    {
        [SerializeField] private float _multiplier = 1f;
        [Min(0f)] [SerializeField] private float _verticalBoost = 1f;
        [SerializeField] private bool _useImpulse = true;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Debug.Assert(_rigidbody != null, "[Knockback] _rigidbody is null"); // R5
        }

        public void Apply(Vector3 origin, float force)
        {
            Debug.Assert(force >= 0f, "[Knockback.Apply] force is negative"); // R5
            if (_rigidbody == null || force <= 0f) return;

            Vector3 dir = (transform.position - origin);
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.001f) dir = -transform.forward;
            dir.Normalize();
            dir.y = _verticalBoost;
            dir.Normalize();

            Vector3 impulse = dir * force * _multiplier;
            if (_useImpulse) _rigidbody.AddForce(impulse, ForceMode.Impulse);
            else _rigidbody.linearVelocity = impulse;
        }
    }
}
