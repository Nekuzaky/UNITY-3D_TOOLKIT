using UnityEngine;

namespace GameJamToolkit.Data
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Data/DifficultyConfig", fileName = "DifficultyConfig")]
    public sealed class DifficultyConfig : ScriptableObject
    {
        public string Name = "Normal";
        [Min(0.1f)] public float EnemyHealthMultiplier = 1f;
        [Min(0.1f)] public float EnemyDamageMultiplier = 1f;
        [Min(0.1f)] public float EnemySpeedMultiplier = 1f;
        [Min(0.1f)] public float ScoreMultiplier = 1f;
        [Min(0.1f)] public float EnemyCountMultiplier = 1f;
    }
}
