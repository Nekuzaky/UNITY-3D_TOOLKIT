using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Wire alongside a HealthComponent: on death adds score and publishes
    /// an EnemyKilledEvent. No loot Instantiate here (see LootDropper).
    /// </summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class KillReward : MonoBehaviour
    {
        [SerializeField] private int _scoreReward = 100;

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[KillReward] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDied += HandleDeath; }
        private void OnDisable() { _health.OnDied -= HandleDeath; }

        private void HandleDeath()
        {
            if (ScoreManager.HasInstance && _scoreReward > 0) ScoreManager.Instance.Add(_scoreReward);
            EventBus.Publish(new EnemyKilledEvent { Enemy = gameObject, Position = transform.position, ScoreReward = _scoreReward });
        }
    }
}
