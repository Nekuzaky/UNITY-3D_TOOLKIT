using UnityEngine;

namespace GameJamToolkit.Progression
{
    /// <summary>XP-per-level table. Returns the XP required for a given level.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Progression/LevelTable", fileName = "LevelTable")]
    public sealed class LevelTable : ScriptableObject
    {
        [SerializeField] private int[] _xpPerLevel = new[] { 0, 100, 250, 500, 1000, 2000, 4000, 7000, 11000, 16000 };

        public int MaxLevel => _xpPerLevel != null ? _xpPerLevel.Length - 1 : 0;

        public int XpForLevel(int level)
        {
            Debug.Assert(_xpPerLevel != null && _xpPerLevel.Length > 0, "[LevelTable.XpForLevel] _xpPerLevel is empty"); // R5
            if (_xpPerLevel == null || _xpPerLevel.Length == 0) return 0;
            int safe = Mathf.Clamp(level, 0, _xpPerLevel.Length - 1);
            return _xpPerLevel[safe];
        }

        public int LevelForXp(int xp)
        {
            if (_xpPerLevel == null) return 0;
            int max = _xpPerLevel.Length; // R2
            int level = 0;
            for (int i = 0; i < max; i++)
            {
                if (xp >= _xpPerLevel[i]) level = i;
                else break;
            }
            return level;
        }
    }
}
