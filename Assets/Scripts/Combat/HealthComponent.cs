using System;
using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Universal HP component. Handles damage, healing, invulnerability,
    /// per-type resistances. Source of truth wired through IDamageable.
    /// Publishes EventBus events for UI/audio/VFX decoupling.
    /// </summary>
    public sealed class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [Header("Setup")]
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private bool _startFull = true;
        [Min(0f)] [SerializeField] private float _invincibilityAfterHit = 0.1f;

        [Header("Resistances (damage multipliers; 0 = immune, 1 = normal, 2 = double)")]
        [SerializeField] private float _physicalMult = 1f;
        [SerializeField] private float _fireMult = 1f;
        [SerializeField] private float _iceMult = 1f;
        [SerializeField] private float _lightningMult = 1f;
        [SerializeField] private float _poisonMult = 1f;

        public float Current { get; private set; }
        public float Max => _maxHealth;
        public float Ratio => _maxHealth > 0f ? Current / _maxHealth : 0f;
        public bool IsAlive => Current > 0f;
        public GameObject GameObject => gameObject;

        public event Action<float, float> OnHealthChanged;
        public event Action<DamageInfo> OnDamageTaken;
        public event Action OnDied;

        private float _invincibleUntil;

        private void Awake()
        {
            Debug.Assert(_maxHealth > 0f, "[HealthComponent] _maxHealth must be > 0"); // R5
            Current = _startFull ? _maxHealth : Mathf.Clamp(Current, 0f, _maxHealth);
        }

        public void TakeDamage(DamageInfo info)
        {
            if (!IsAlive) return;
            if (Time.time < _invincibleUntil) return;
            if (info.Amount <= 0f) return; // R5

            float mult = GetMultiplier(info.Type);
            float final = info.Amount * mult;
            float prev = Current;
            Current = Mathf.Max(0f, Current - final);
            _invincibleUntil = Time.time + _invincibilityAfterHit;

            OnDamageTaken?.Invoke(info);
            OnHealthChanged?.Invoke(Current, _maxHealth);
            EventBus.Publish(new DamageDealtEvent { Source = info.Source, Target = gameObject, Amount = final, HitPoint = info.HitPoint });

            if (prev > 0f && Current <= 0f)
            {
                OnDied?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            Debug.Assert(amount >= 0f, "[HealthComponent.Heal] amount is negative"); // R5
            if (!IsAlive || amount <= 0f) return;
            float prev = Current;
            Current = Mathf.Min(_maxHealth, Current + amount);
            if (!Mathf.Approximately(prev, Current))
            {
                OnHealthChanged?.Invoke(Current, _maxHealth);
                EventBus.Publish(new HealEvent { Target = gameObject, Amount = Current - prev });
            }
        }

        public void Kill()
        {
            if (!IsAlive) return;
            Current = 0f;
            OnHealthChanged?.Invoke(Current, _maxHealth);
            OnDied?.Invoke();
        }

        public void SetMaxHealth(float value, bool refill)
        {
            Debug.Assert(value > 0f, "[HealthComponent.SetMaxHealth] value <= 0"); // R5
            _maxHealth = Mathf.Max(1f, value);
            if (refill) Current = _maxHealth;
            OnHealthChanged?.Invoke(Current, _maxHealth);
        }

        private float GetMultiplier(DamageType type)
        {
            switch (type)
            {
                case DamageType.Physical: return _physicalMult;
                case DamageType.Fire: return _fireMult;
                case DamageType.Ice: return _iceMult;
                case DamageType.Lightning: return _lightningMult;
                case DamageType.Poison: return _poisonMult;
                case DamageType.True: return 1f;
                default: return 1f;
            }
        }
    }
}
