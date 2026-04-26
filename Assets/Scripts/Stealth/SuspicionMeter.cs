using System;
using UnityEngine;

namespace GameJamToolkit.Stealth
{
    /// <summary>
    /// Per-NPC suspicion accumulator. Increases when the player is seen,
    /// decays otherwise. Crosses thresholds to switch AwarenessLevel.
    /// </summary>
    public sealed class SuspicionMeter : MonoBehaviour
    {
        [Min(0f)] [SerializeField] private float _max = 100f;
        [Min(0f)] [SerializeField] private float _decayPerSecond = 8f;
        [SerializeField] private float _suspiciousThreshold = 25f;
        [SerializeField] private float _investigatingThreshold = 60f;
        [SerializeField] private float _alertThreshold = 95f;

        public float Current { get; private set; }
        public AwarenessLevel Level { get; private set; }
        public event Action<AwarenessLevel> OnLevelChanged;

        public void Add(float amount)
        {
            Debug.Assert(amount >= 0f, "[SuspicionMeter.Add] amount is negative"); // R5
            Current = Mathf.Min(_max, Current + amount);
            UpdateLevel();
        }

        private void Update()
        {
            if (Current <= 0f) return;
            Current = Mathf.Max(0f, Current - _decayPerSecond * Time.deltaTime);
            UpdateLevel();
        }

        private void UpdateLevel()
        {
            AwarenessLevel target;
            if (Current >= _alertThreshold) target = AwarenessLevel.Alert;
            else if (Current >= _investigatingThreshold) target = AwarenessLevel.Investigating;
            else if (Current >= _suspiciousThreshold) target = AwarenessLevel.Suspicious;
            else target = AwarenessLevel.Unaware;

            if (target == Level) return;
            Level = target;
            OnLevelChanged?.Invoke(Level);
        }
    }
}
