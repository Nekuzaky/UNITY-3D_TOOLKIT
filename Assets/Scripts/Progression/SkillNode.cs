using UnityEngine;

namespace GameJamToolkit.Progression
{
    /// <summary>A single skill node in a skill tree. SO so designers can author trees in editor.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Progression/SkillNode", fileName = "SkillNode")]
    public sealed class SkillNode : ScriptableObject
    {
        public string SkillId = "skill_default";
        public string DisplayName = "Skill";
        [TextArea] public string Description;
        public Sprite Icon;
        [Min(1)] public int Cost = 1;
        public SkillNode[] Prerequisites;
    }
}
