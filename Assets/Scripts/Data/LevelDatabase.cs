using UnityEngine;

namespace GameJamToolkit.Data
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Data/LevelDatabase", fileName = "LevelDatabase")]
    public sealed class LevelDatabase : ScriptableObject
    {
        [SerializeField] private LevelConfig[] _levels;

        public LevelConfig[] Levels => _levels;

        public LevelConfig GetById(string id)
        {
            if (_levels == null) return null;
            int max = _levels.Length;
            for (int i = 0; i < max; i++)
            {
                if (_levels[i] != null && _levels[i].LevelId == id) return _levels[i];
            }
            return null;
        }
    }
}
