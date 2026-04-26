using UnityEngine;

namespace GameJamToolkit.Data
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Data/LevelConfig", fileName = "LevelConfig")]
    public sealed class LevelConfig : ScriptableObject
    {
        public string LevelId;
        public string SceneName;
        public string DisplayName;
        [TextArea] public string Description;
        public Sprite Thumbnail;
        public int RequiredStarsToUnlock = 0;
        public int Order = 0;
    }
}
