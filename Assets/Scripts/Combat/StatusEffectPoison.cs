using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Poison: Poison damage over time, ignores physical armor.</summary>
    public sealed class StatusEffectPoison : StatusEffectBase
    {
        [SerializeField] private float _damagePerTick = 2f;
        private IDamageable _target;

        protected override void OnApply()
        {
            _target = GetComponentInParent<IDamageable>();
            Debug.Assert(_target != null, "[StatusEffectPoison] _target is null"); // R5
        }

        protected override void OnTick()
        {
            if (_target == null || !_target.IsAlive) return;
            _target.TakeDamage(new DamageInfo
            {
                Amount = _damagePerTick,
                Type = DamageType.Poison,
                Source = _source != null ? _source : gameObject,
                HitPoint = transform.position,
                HitNormal = Vector3.up
            });
        }
    }
}
