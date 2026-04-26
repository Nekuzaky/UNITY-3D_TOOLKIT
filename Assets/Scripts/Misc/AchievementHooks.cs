using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Misc
{
    /// <summary>Default hooks: "first_kill" on EnemyKilled, "first_death" on PlayerDied.</summary>
    public sealed class AchievementHooks : MonoBehaviour
    {
        [SerializeField] private string _firstKillId = "first_kill";
        [SerializeField] private string _firstDeathId = "first_death";

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
        }

        private void OnEnemyKilled(EnemyKilledEvent e)
        {
            if (AchievementManager.HasInstance) AchievementManager.Instance.Unlock(_firstKillId);
        }

        private void OnPlayerDied(PlayerDiedEvent e)
        {
            if (AchievementManager.HasInstance) AchievementManager.Instance.Unlock(_firstDeathId);
        }
    }
}
