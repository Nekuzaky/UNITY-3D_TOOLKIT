using System;
using System.Collections.Generic;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Misc
{
    /// <summary>
    /// Achievement tracking: PlayerPrefs flag + public event. Matching with
    /// game conditions is done in project code (e.g. NotifyAchievement("kill_100")).
    /// </summary>
    public sealed class AchievementManager : PersistentSingleton<AchievementManager>
    {
        [SerializeField] private AchievementBase[] _achievements;
        public event Action<AchievementBase> OnAchievementUnlocked;

        private const string PrefsPrefix = "Ach_";

        public bool IsUnlocked(string id) { return PlayerPrefs.GetInt(PrefsPrefix + id, 0) == 1; }

        public void Unlock(string id)
        {
            Debug.Assert(!string.IsNullOrEmpty(id), "[AchievementManager.Unlock] id is empty"); // R5
            if (IsUnlocked(id)) return;
            PlayerPrefs.SetInt(PrefsPrefix + id, 1);
            PlayerPrefs.Save();
            OnAchievementUnlocked?.Invoke(GetById(id));
        }

        public AchievementBase GetById(string id)
        {
            if (_achievements == null) return null;
            int max = _achievements.Length; // R2
            for (int i = 0; i < max; i++)
            {
                if (_achievements[i] != null && _achievements[i].AchievementId == id) return _achievements[i];
            }
            return null;
        }

        public IReadOnlyList<AchievementBase> All => _achievements;
    }
}
