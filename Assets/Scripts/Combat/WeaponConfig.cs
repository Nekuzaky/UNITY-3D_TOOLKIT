using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>External weapon config. Allows multiple presets for the same prefab.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Combat/WeaponConfig", fileName = "WeaponConfig")]
    public sealed class WeaponConfig : ScriptableObject
    {
        [Header("Identity")]
        public string DisplayName = "Weapon";
        public Sprite Icon;

        [Header("Shots")]
        [Min(0f)] public float Damage = 10f;
        public DamageType Type = DamageType.Physical;
        [Min(0f)] public float Cooldown = 0.3f;
        [Min(0f)] public float ProjectileSpeed = 25f;
        [Min(0f)] public float Range = 30f;
        [Min(0)] public int Pellets = 1;
        [Min(0f)] public float Spread = 0f;

        [Header("Resources")]
        [Min(0f)] public float StaminaCost = 0f;
        [Min(0)] public int Magazine = 0;
        [Min(0f)] public float ReloadSeconds = 0f;
    }
}
