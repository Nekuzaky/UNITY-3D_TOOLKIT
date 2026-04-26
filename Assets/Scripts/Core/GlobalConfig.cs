using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Global config ScriptableObject. Centralizes project constants (reference
    /// speeds, gravity, distances, etc.) to avoid hardcoded values.
    /// </summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Core/GlobalConfig", fileName = "GlobalConfig")]
    public sealed class GlobalConfig : ScriptableObject
    {
        [Header("Frame rate")]
        [Min(15)] public int TargetFrameRate = 60;
        public bool VSync = true;

        [Header("Physics")]
        public float Gravity3D = -9.81f;
        public LayerMask GroundMask;
        public LayerMask EnemyMask;
        public LayerMask PlayerMask;
        public LayerMask InteractableMask;

        [Header("Default game values")]
        [Min(0f)] public float DefaultPlayerSpeed = 5f;
        [Min(0f)] public float DefaultJumpForce = 8f;
        [Min(0f)] public float DefaultDamage = 10f;
    }
}
