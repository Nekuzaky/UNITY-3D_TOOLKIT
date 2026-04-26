using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Misc
{
    /// <summary>Switches to GameOver as soon as a PlayerDiedEvent is published.</summary>
    public sealed class GameOverWatcher : MonoBehaviour
    {
        private void OnEnable() { EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied); }
        private void OnDisable() { EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied); }

        private void HandlePlayerDied(PlayerDiedEvent evt)
        {
            if (GameManager.HasInstance) GameManager.Instance.TriggerGameOver();
        }
    }
}
