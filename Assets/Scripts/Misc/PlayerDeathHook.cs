using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Misc
{
    /// <summary>Bridges HealthComponent.OnDied -> publishes PlayerDiedEvent.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class PlayerDeathHook : MonoBehaviour
    {
        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[PlayerDeathHook] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDied += HandleDied; }
        private void OnDisable() { _health.OnDied -= HandleDied; }

        private void HandleDied()
        {
            float score = ScoreManager.HasInstance ? ScoreManager.Instance.CurrentScore : 0f;
            EventBus.Publish(new PlayerDiedEvent { Player = gameObject, Position = transform.position, Score = score });
        }
    }
}
