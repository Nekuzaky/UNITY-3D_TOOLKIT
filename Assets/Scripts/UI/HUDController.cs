using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Player;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.UI
{
    /// <summary>Auto-binds HUD widgets to the spawned player.</summary>
    public sealed class HUDController : MonoBehaviour
    {
        [SerializeField] private HealthBarUI _healthBar;
        [SerializeField] private StaminaBarUI _staminaBar;

        private void OnEnable() { EventBus.Subscribe<PlayerSpawnedEvent>(HandlePlayerSpawned); }
        private void OnDisable() { EventBus.Unsubscribe<PlayerSpawnedEvent>(HandlePlayerSpawned); }

        private void HandlePlayerSpawned(PlayerSpawnedEvent evt)
        {
            if (evt.Player == null) return;
            if (_healthBar != null)
            {
                var h = evt.Player.GetComponent<HealthComponent>();
                if (h != null) _healthBar.SetSource(h);
            }
            if (_staminaBar != null)
            {
                var s = evt.Player.GetComponent<PlayerStamina>();
                if (s != null) _staminaBar.SetSource(s);
            }
        }
    }
}
