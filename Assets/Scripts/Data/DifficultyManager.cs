using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Data
{
    /// <summary>Holds the active <see cref="DifficultyConfig"/>. Globally accessible.</summary>
    public sealed class DifficultyManager : PersistentSingleton<DifficultyManager>
    {
        [SerializeField] private DifficultyConfig _defaultConfig;
        public DifficultyConfig Active { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Active = _defaultConfig;
        }

        public void SetDifficulty(DifficultyConfig config)
        {
            Debug.Assert(config != null, "[DifficultyManager.SetDifficulty] config is null"); // R5
            if (config == null) return;
            Active = config;
        }
    }
}
