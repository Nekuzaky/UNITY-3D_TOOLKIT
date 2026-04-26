using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Generic player stats. No logic: pure runtime data exposed via
    /// public properties for UI / other modules to read.
    /// Initialized by <see cref="PlayerStatsConfig"/>.
    /// </summary>
    public sealed class PlayerStats : MonoBehaviour
    {
        [SerializeField] private PlayerStatsConfig _config;

        public float MoveSpeed { get; private set; }
        public float SprintMultiplier { get; private set; }
        public float JumpForce { get; private set; }
        public float Acceleration { get; private set; }
        public float Deceleration { get; private set; }
        public float MaxHealth { get; private set; }
        public float MaxStamina { get; private set; }
        public float StaminaRegen { get; private set; }
        public float DashForce { get; private set; }
        public float DashCooldown { get; private set; }

        private void Awake()
        {
            Debug.Assert(_config != null, "[PlayerStats] _config is null"); // R5
            ApplyConfig();
        }

        public void ApplyConfig()
        {
            if (_config == null) return;
            MoveSpeed = _config.MoveSpeed;
            SprintMultiplier = _config.SprintMultiplier;
            JumpForce = _config.JumpForce;
            Acceleration = _config.Acceleration;
            Deceleration = _config.Deceleration;
            MaxHealth = _config.MaxHealth;
            MaxStamina = _config.MaxStamina;
            StaminaRegen = _config.StaminaRegen;
            DashForce = _config.DashForce;
            DashCooldown = _config.DashCooldown;
        }

        public void OverrideMoveSpeed(float value) { MoveSpeed = Mathf.Max(0f, value); }
        public void OverrideJumpForce(float value) { JumpForce = Mathf.Max(0f, value); }
    }
}
