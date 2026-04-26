using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Misc
{
    /// <summary>Publishes EnemyKilledEvent when an enemy HealthComponent dies.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class EnemyKilledHook : MonoBehaviour
    {
        [SerializeField] private int _scoreReward = 100;

        private HealthComponent _health;

        private void Awake() { _health = GetComponent<HealthComponent>(); Debug.Assert(_health != null, "[EnemyKilledHook] _health is null"); }
        private void OnEnable() { _health.OnDied += HandleDied; }
        private void OnDisable() { _health.OnDied -= HandleDied; }

        private void HandleDied()
        {
            EventBus.Publish(new EnemyKilledEvent { Enemy = gameObject, Position = transform.position, ScoreReward = _scoreReward });
        }
    }
}
