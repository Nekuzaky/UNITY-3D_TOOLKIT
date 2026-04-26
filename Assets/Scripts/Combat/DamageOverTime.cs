using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Damage-over-time effect. Stick on the target for the desired time, or
    /// use as StatusEffectBurn / StatusEffectPoison.
    /// </summary>
    public sealed class DamageOverTime : MonoBehaviour
    {
        [SerializeField] private IDamageableHolder _holder;
        [SerializeField] private float _damagePerTick = 2f;
        [SerializeField] private float _tickInterval = 0.5f;
        [SerializeField] private float _totalDuration = 4f;
        [SerializeField] private DamageType _type = DamageType.Fire;
        [SerializeField] private GameObject _source;

        private float _nextTickAt;
        private float _expiresAt;
        private IDamageable _damageable;

        private void Awake()
        {
            _damageable = _holder != null ? _holder.Get() : GetComponentInParent<IDamageable>();
            Debug.Assert(_damageable != null, "[DamageOverTime] _damageable is null"); // R5
            _expiresAt = Time.time + _totalDuration;
            _nextTickAt = Time.time + _tickInterval;
        }

        private void Update()
        {
            if (_damageable == null) { Destroy(this); return; }
            if (Time.time >= _expiresAt) { Destroy(this); return; }
            if (Time.time < _nextTickAt) return;
            _nextTickAt = Time.time + _tickInterval;
            _damageable.TakeDamage(new DamageInfo
            {
                Amount = _damagePerTick,
                Type = _type,
                Source = _source != null ? _source : gameObject,
                HitPoint = transform.position,
                HitNormal = Vector3.up
            });
        }

        public void Configure(float dmgTick, float interval, float duration, DamageType type, GameObject source)
        {
            _damagePerTick = Mathf.Max(0f, dmgTick);
            _tickInterval = Mathf.Max(0.05f, interval);
            _totalDuration = Mathf.Max(0f, duration);
            _type = type;
            _source = source;
            _expiresAt = Time.time + _totalDuration;
            _nextTickAt = Time.time + _tickInterval;
        }
    }
}
