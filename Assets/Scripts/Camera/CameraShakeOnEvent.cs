using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Triggers a camera shake from EventBus events.</summary>
    public sealed class CameraShakeOnEvent : MonoBehaviour
    {
        [SerializeField] private float _onPlayerDamageTrauma = 0.4f;
        [SerializeField] private float _onEnemyKilledTrauma = 0.15f;
        [SerializeField] private float _onPlayerDiedTrauma = 0.8f;

        private void OnEnable()
        {
            EventBus.Subscribe<DamageDealtEvent>(OnDamage);
            EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<DamageDealtEvent>(OnDamage);
            EventBus.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        private void OnDamage(DamageDealtEvent evt)
        {
            if (CameraShake.ActiveInstance == null) return;
            // Shake only if the target is the player (heuristic)
            if (evt.Target != null && evt.Target.CompareTag("Player")) CameraShake.ActiveInstance.Shake(_onPlayerDamageTrauma);
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            if (CameraShake.ActiveInstance != null) CameraShake.ActiveInstance.Shake(_onEnemyKilledTrauma);
        }

        private void OnPlayerDied(PlayerDiedEvent evt)
        {
            if (CameraShake.ActiveInstance != null) CameraShake.ActiveInstance.Shake(_onPlayerDiedTrauma);
        }
    }
}
