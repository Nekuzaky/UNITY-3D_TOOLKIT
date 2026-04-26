using UnityEngine;
using UnityEngine.UI;
using GameJamToolkit.Combat;

namespace GameJamToolkit.UI
{
    /// <summary>HUD health bar. Connect to the player's HealthComponent.</summary>
    public sealed class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private HealthComponent _source;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Gradient _fillGradient;

        private void Awake()
        {
            Debug.Assert(_slider != null, "[HealthBarUI] _slider is null"); // R5
        }

        private void OnEnable()
        {
            if (_source != null) _source.OnHealthChanged += Refresh;
            if (_source != null) Refresh(_source.Current, _source.Max);
        }

        private void OnDisable()
        {
            if (_source != null) _source.OnHealthChanged -= Refresh;
        }

        public void SetSource(HealthComponent source)
        {
            if (_source != null) _source.OnHealthChanged -= Refresh;
            _source = source;
            if (_source != null) { _source.OnHealthChanged += Refresh; Refresh(_source.Current, _source.Max); }
        }

        private void Refresh(float current, float max)
        {
            if (_slider == null) return;
            _slider.maxValue = max;
            _slider.value = current;
            if (_fillImage != null && _fillGradient != null)
            {
                float ratio = max > 0f ? current / max : 0f;
                _fillImage.color = _fillGradient.Evaluate(ratio);
            }
        }
    }
}
