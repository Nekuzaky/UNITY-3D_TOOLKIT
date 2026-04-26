using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Survival
{
    /// <summary>When a Need is fully depleted, ticks damage to a HealthComponent.</summary>
    [RequireComponent(typeof(Need))]
    public sealed class NeedDamageOnDeplete : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private float _damagePerTick = 2f;

        private Need _need;

        private void Awake()
        {
            _need = GetComponent<Need>();
            if (_health == null) _health = GetComponentInParent<HealthComponent>();
            Debug.Assert(_need != null, "[NeedDamageOnDeplete] _need is null"); // R5
            Debug.Assert(_health != null, "[NeedDamageOnDeplete] _health is null"); // R5
        }

        private void OnEnable() { _need.OnDepletedTick += HandleTick; }
        private void OnDisable() { _need.OnDepletedTick -= HandleTick; }

        private void HandleTick()
        {
            if (_health == null) return;
            _health.TakeDamage(DamageInfo.Default(_damagePerTick, gameObject));
        }
    }
}
