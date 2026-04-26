using UnityEngine;
using UnityEngine.UI;
using GameJamToolkit.Player;

namespace GameJamToolkit.UI
{
    /// <summary>Stamina bar.</summary>
    public sealed class StaminaBarUI : MonoBehaviour
    {
        [SerializeField] private PlayerStamina _source;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            Debug.Assert(_slider != null, "[StaminaBarUI] _slider is null"); // R5
        }

        private void OnEnable() { if (_source != null) _source.OnStaminaChanged += Refresh; }
        private void OnDisable() { if (_source != null) _source.OnStaminaChanged -= Refresh; }

        public void SetSource(PlayerStamina source)
        {
            if (_source != null) _source.OnStaminaChanged -= Refresh;
            _source = source;
            if (_source != null) { _source.OnStaminaChanged += Refresh; Refresh(_source.Current, _source.Max); }
        }

        private void Refresh(float current, float max)
        {
            if (_slider == null) return;
            _slider.maxValue = max;
            _slider.value = current;
        }
    }
}
