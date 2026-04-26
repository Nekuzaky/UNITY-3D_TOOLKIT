using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>
    /// Abstract enemy class. All project logic derives from it.
    /// Pairs a HealthComponent + EnemyConfig + optional AI.
    /// </summary>
    [RequireComponent(typeof(HealthComponent))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected EnemyConfig _config;
        [SerializeField] protected Transform _target;

        protected HealthComponent _health;
        public HealthComponent Health => _health;
        public Transform Target { get => _target; set => _target = value; }
        public EnemyConfig Config => _config;

        protected virtual void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[EnemyBase] _health is null"); // R5
            ApplyConfig();
        }

        protected virtual void ApplyConfig()
        {
            if (_config == null) return;
            _health.SetMaxHealth(_config.MaxHealth, true);
        }

        public virtual void SetTarget(Transform t) { _target = t; }
    }
}
