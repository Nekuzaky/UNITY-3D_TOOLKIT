using System;
using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>Regenerative stamina. Single source of truth for Sprint, Dash, etc.</summary>
    public sealed class PlayerStamina : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [Min(0f)] [SerializeField] private float _regenDelayAfterUse = 0.6f;

        public float Current { get; private set; }
        public float Max => _stats != null ? _stats.MaxStamina : 100f;
        public float Ratio => Max > 0f ? Current / Max : 0f;
        public event Action<float, float> OnStaminaChanged;

        private float _nextRegenAt;

        private void Awake()
        {
            if (_stats == null) _stats = GetComponent<PlayerStats>();
            Debug.Assert(_stats != null, "[PlayerStamina] _stats is null"); // R5
            Current = Max;
        }

        private void Update()
        {
            if (Time.time < _nextRegenAt) return;
            if (Current >= Max) return;
            float prev = Current;
            Current = Mathf.Min(Max, Current + _stats.StaminaRegen * Time.deltaTime);
            if (!Mathf.Approximately(prev, Current)) OnStaminaChanged?.Invoke(Current, Max);
        }

        public bool Consume(float amount)
        {
            Debug.Assert(amount >= 0f, "[PlayerStamina.Consume] amount is negative"); // R5
            if (Current < amount) return false;
            float prev = Current;
            Current = Mathf.Max(0f, Current - amount);
            _nextRegenAt = Time.time + _regenDelayAfterUse;
            if (!Mathf.Approximately(prev, Current)) OnStaminaChanged?.Invoke(Current, Max);
            return true;
        }

        public void Restore(float amount)
        {
            float prev = Current;
            Current = Mathf.Min(Max, Current + Mathf.Max(0f, amount));
            if (!Mathf.Approximately(prev, Current)) OnStaminaChanged?.Invoke(Current, Max);
        }
    }
}
