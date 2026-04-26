using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Burn effect: Fire damage per tick.</summary>
    public sealed class StatusEffectBurn : StatusEffectBase
    {
        [SerializeField] private float _damagePerTick = 3f;
        private IDamageable _target;

        protected override void OnApply()
        {
            _target = GetComponentInParent<IDamageable>();
            Debug.Assert(_target != null, "[StatusEffectBurn] _target is null"); // R5
        }

        protected override void OnTick()
        {
            if (_target == null || !_target.IsAlive) return;
            _target.TakeDamage(new DamageInfo
            {
                Amount = _damagePerTick,
                Type = DamageType.Fire,
                Source = _source != null ? _source : gameObject,
                HitPoint = transform.position,
                HitNormal = Vector3.up
            });
        }

        public void SetDamagePerTick(float value) { _damagePerTick = Mathf.Max(0f, value); }
    }
}
