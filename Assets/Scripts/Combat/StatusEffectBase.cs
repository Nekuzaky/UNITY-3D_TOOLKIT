using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Abstract base for any status effect (burn, freeze, stun...).
    /// Cycle: OnApply -> OnTick (every _tickInterval) -> OnExpire.
    /// </summary>
    public abstract class StatusEffectBase : MonoBehaviour
    {
        [SerializeField] protected float _duration = 3f;
        [SerializeField] protected float _tickInterval = 0.5f;

        protected float _expiresAt;
        protected float _nextTickAt;
        protected GameObject _source;

        public bool IsActive => Time.time < _expiresAt;

        public virtual void Apply(GameObject source, float duration, float tickInterval)
        {
            Debug.Assert(duration > 0f, "[StatusEffectBase.Apply] duration <= 0"); // R5
            _source = source;
            _duration = Mathf.Max(0.05f, duration);
            _tickInterval = Mathf.Max(0.05f, tickInterval);
            _expiresAt = Time.time + _duration;
            _nextTickAt = Time.time + _tickInterval;
            OnApply();
        }

        protected virtual void Update()
        {
            if (!IsActive) { OnExpire(); Destroy(this); return; }
            if (Time.time < _nextTickAt) return;
            _nextTickAt = Time.time + _tickInterval;
            OnTick();
        }

        protected virtual void OnApply() { }
        protected virtual void OnTick() { }
        protected virtual void OnExpire() { }
    }
}
