using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>External Player config. Editable outside code.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Player/StatsConfig", fileName = "PlayerStatsConfig")]
    public sealed class PlayerStatsConfig : ScriptableObject
    {
        [Header("Movement")]
        [Min(0f)] public float MoveSpeed = 5f;
        [Min(1f)] public float SprintMultiplier = 1.6f;
        [Min(0f)] public float JumpForce = 8f;
        [Min(0f)] public float Acceleration = 50f;
        [Min(0f)] public float Deceleration = 60f;

        [Header("Combat")]
        [Min(1f)] public float MaxHealth = 100f;
        [Min(0f)] public float MaxStamina = 100f;
        [Min(0f)] public float StaminaRegen = 20f;

        [Header("Abilities")]
        [Min(0f)] public float DashForce = 12f;
        [Min(0f)] public float DashCooldown = 1.2f;
    }
}
