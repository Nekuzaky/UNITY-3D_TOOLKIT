using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>Detects the player when damaged: stores the source and notifies the AI.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class AggroSystem : MonoBehaviour
    {
        [SerializeField] private EnemyBase _enemy;
        [SerializeField] private float _decaySeconds = 6f;

        private HealthComponent _health;
        private Transform _aggroTarget;
        private float _lastAggroAt;

        public Transform AggroTarget => _aggroTarget;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            if (_enemy == null) _enemy = GetComponent<EnemyBase>();
            Debug.Assert(_health != null, "[AggroSystem] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDamageTaken += HandleDamage; }
        private void OnDisable() { _health.OnDamageTaken -= HandleDamage; }

        private void HandleDamage(DamageInfo info)
        {
            if (info.Source == null) return;
            _aggroTarget = info.Source.transform;
            _lastAggroAt = Time.time;
            if (_enemy != null) _enemy.SetTarget(_aggroTarget);
        }

        private void Update()
        {
            if (_aggroTarget == null) return;
            if (Time.time - _lastAggroAt >= _decaySeconds)
            {
                _aggroTarget = null;
                if (_enemy != null) _enemy.SetTarget(null);
            }
        }
    }
}
