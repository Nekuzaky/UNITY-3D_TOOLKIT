using UnityEngine;

namespace GameJamToolkit.Misc
{
    /// <summary>Achievement / success definition.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Misc/Achievement", fileName = "Achievement")]
    public sealed class AchievementBase : ScriptableObject
    {
        public string AchievementId;
        public string Title;
        [TextArea] public string Description;
        public Sprite Icon;
        public bool Hidden;
    }
}
