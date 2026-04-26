using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJamToolkit.UI;

namespace GameJamToolkit.Misc
{
    /// <summary>Shows a notification when an achievement is unlocked.</summary>
    public sealed class AchievementToastUI : MonoBehaviour
    {
        [SerializeField] private NotificationUI _notification;

        private void OnEnable()
        {
            if (AchievementManager.HasInstance) AchievementManager.Instance.OnAchievementUnlocked += Show;
        }

        private void OnDisable()
        {
            if (AchievementManager.HasInstance) AchievementManager.Instance.OnAchievementUnlocked -= Show;
        }

        private void Show(AchievementBase a)
        {
            if (a == null || _notification == null) return;
            _notification.Push($"{a.Title}");
        }
    }
}
