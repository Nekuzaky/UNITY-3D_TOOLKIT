using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Active hitbox: damages every entering IDamageable.
    /// No re-apply per target before cooldown (ticks).
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class HitboxController : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private DamageType _damageType = DamageType.Physical;
        [SerializeField] private float _knockback = 5f;
        [SerializeField] private LayerMask _targetMask = ~0;
        [Min(0f)] [SerializeField] private float _tickInterval = 0.5f;
        [SerializeField] private GameObject _owner;
        [SerializeField] private bool _activeAtStart = true;

        private readonly Dictionary<IDamageable, float> _nextHitDict = new Dictionary<IDamageable, float>();
        private bool _isActive;

        public bool IsActive => _isActive;

        private void Awake()
        {
            Debug.Assert(_damage > 0f, "[HitboxController] _damage <= 0"); // R5
            Debug.Assert(GetComponent<Collider>().isTrigger, "[HitboxController] the collider must be a trigger"); // R5
            _isActive = _activeAtStart;
        }

        public void Activate() { _isActive = true; _nextHitDict.Clear(); }
        public void Deactivate() { _isActive = false; }

        private void OnTriggerEnter(Collider other) { TryHit(other); }
        private void OnTriggerStay(Collider other) { TryHit(other); }

        private void TryHit(Collider other)
        {
            if (!_isActive || other == null) return;
            if (((1 << other.gameObject.layer) & _targetMask) == 0) return;

            var damageable = other.GetComponentInParent<IDamageable>();
            if (damageable == null || !damageable.IsAlive) return;
            if (_owner != null && damageable.GameObject == _owner) return; // self-damage off

            if (_nextHitDict.TryGetValue(damageable, out var next) && Time.time < next) return;
            _nextHitDict[damageable] = Time.time + _tickInterval;

            var info = new DamageInfo
            {
                Amount = _damage,
                Type = _damageType,
                Source = _owner != null ? _owner : gameObject,
                HitPoint = other.ClosestPoint(transform.position),
                HitNormal = (other.transform.position - transform.position).normalized,
                Knockback = _knockback,
                IsCritical = false
            };
            damageable.TakeDamage(info);
        }
    }
}
