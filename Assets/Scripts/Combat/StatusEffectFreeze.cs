using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Freeze: slows down as Rigidbody drag. Does not remove control;
    /// use together with <see cref="StatusEffectStun"/> to fully block.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class StatusEffectFreeze : StatusEffectBase
    {
        [Range(0f, 1f)] [SerializeField] private float _slowMultiplier = 0.3f;

        private Rigidbody _rigidbody;
        private float _originalDrag;

        protected override void OnApply()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
            Debug.Assert(_rigidbody != null, "[StatusEffectFreeze] _rigidbody is null"); // R5
            if (_rigidbody == null) return;
            _originalDrag = _rigidbody.linearDamping;
            _rigidbody.linearVelocity *= _slowMultiplier;
            _rigidbody.linearDamping = Mathf.Max(_originalDrag, 4f);
        }

        protected override void OnExpire()
        {
            if (_rigidbody == null) return;
            _rigidbody.linearDamping = _originalDrag;
        }
    }
}
