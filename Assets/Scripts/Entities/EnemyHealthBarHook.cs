using UnityEngine;
using UnityEngine.UI;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>Binds a Unity UI Slider to a HealthComponent (local to the enemy, not HUD).</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class EnemyHealthBarHook : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private bool _hideAtFull = true;

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[EnemyHealthBarHook] _health is null"); // R5
            Debug.Assert(_slider != null, "[EnemyHealthBarHook] _slider is null"); // R5
        }

        private void OnEnable() { _health.OnHealthChanged += Refresh; Refresh(_health.Current, _health.Max); }
        private void OnDisable() { _health.OnHealthChanged -= Refresh; }

        private void Refresh(float current, float max)
        {
            if (_slider == null) return;
            _slider.maxValue = max;
            _slider.value = current;
            if (_hideAtFull) _slider.gameObject.SetActive(current < max && current > 0f);
        }
    }
}
