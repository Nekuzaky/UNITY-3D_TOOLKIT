using UnityEngine;

namespace GameJamToolkit.Entities
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Entities/EnemyConfig", fileName = "EnemyConfig")]
    public sealed class EnemyConfig : ScriptableObject
    {
        [Header("Identity")]
        public string DisplayName = "Enemy";

        [Header("Stats")]
        [Min(1f)] public float MaxHealth = 50f;
        [Min(0f)] public float MoveSpeed = 3f;
        [Min(0f)] public float AttackRange = 1.5f;
        [Min(0f)] public float SightRange = 8f;
        [Min(0f)] public float AttackCooldown = 1.0f;
        [Min(0f)] public float Damage = 10f;
        public Combat.DamageType DamageType = Combat.DamageType.Physical;

        [Header("Loot")]
        public int ScoreReward = 100;
    }
}
