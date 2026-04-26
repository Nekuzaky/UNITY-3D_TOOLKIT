using System;
using UnityEngine;

namespace GameJamToolkit.Progression
{
    /// <summary>XP accumulator. Notifies on XP changes and on level up.</summary>
    public sealed class ExperienceSystem : MonoBehaviour
    {
        [SerializeField] private LevelTable _table;
        public int CurrentXp { get; private set; }
        public int CurrentLevel { get; private set; }

        public event Action<int> OnXpChanged;
        public event Action<int> OnLevelUp;

        public void Add(int amount)
        {
            Debug.Assert(amount >= 0, "[ExperienceSystem.Add] amount is negative"); // R5
            Debug.Assert(_table != null, "[ExperienceSystem.Add] _table is null"); // R5
            if (amount <= 0 || _table == null) return;
            CurrentXp += amount;
            OnXpChanged?.Invoke(CurrentXp);
            int newLevel = _table.LevelForXp(CurrentXp);
            while (newLevel > CurrentLevel)
            {
                CurrentLevel++;
                OnLevelUp?.Invoke(CurrentLevel);
            }
        }

        public int XpToNext()
        {
            if (_table == null) return 0;
            int nextLevel = Mathf.Min(_table.MaxLevel, CurrentLevel + 1);
            return Mathf.Max(0, _table.XpForLevel(nextLevel) - CurrentXp);
        }
    }
}
