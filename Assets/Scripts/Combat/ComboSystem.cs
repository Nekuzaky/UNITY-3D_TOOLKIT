using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Combo counter: advances one notch on Notch(), drops back
    /// to 0 if no notch within _resetSeconds. Used for multipliers
    /// on score / attack.
    /// </summary>
    public sealed class ComboSystem : MonoBehaviour
    {
        [Min(0.1f)] [SerializeField] private float _resetSeconds = 2.0f;
        [Min(1)] [SerializeField] private int _maxCombo = 99;

        public int Current { get; private set; }
        public float Multiplier => 1f + Current * 0.1f;
        public event System.Action<int> OnComboChanged;

        private float _expiresAt;

        private void Update()
        {
            if (Current > 0 && Time.time >= _expiresAt) Reset();
        }

        public void Notch()
        {
            Debug.Assert(_resetSeconds > 0f, "[ComboSystem.Notch] _resetSeconds <= 0"); // R5
            Current = Mathf.Min(_maxCombo, Current + 1);
            _expiresAt = Time.time + _resetSeconds;
            OnComboChanged?.Invoke(Current);
        }

        public void Reset()
        {
            if (Current == 0) return;
            Current = 0;
            OnComboChanged?.Invoke(0);
        }
    }
}
