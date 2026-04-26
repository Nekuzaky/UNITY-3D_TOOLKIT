using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>Triggers an AreaDamage on death.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class EnemyDeathExplosion : MonoBehaviour
    {
        [SerializeField] private AreaDamage _explosion;

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[EnemyDeathExplosion] _health is null"); // R5
            Debug.Assert(_explosion != null, "[EnemyDeathExplosion] _explosion is null"); // R5
        }

        private void OnEnable() { _health.OnDied += HandleDeath; }
        private void OnDisable() { _health.OnDied -= HandleDeath; }
        private void HandleDeath() { if (_explosion != null) _explosion.Detonate(transform.position); }
    }
}
