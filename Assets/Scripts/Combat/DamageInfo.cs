using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Struct carrying all details of a damage instance.</summary>
    public struct DamageInfo
    {
        public float Amount;
        public DamageType Type;
        public GameObject Source;
        public Vector3 HitPoint;
        public Vector3 HitNormal;
        public float Knockback;
        public bool IsCritical;

        public static DamageInfo Default(float amount, GameObject source) => new DamageInfo
        {
            Amount = amount,
            Type = DamageType.Physical,
            Source = source,
            HitPoint = Vector3.zero,
            HitNormal = Vector3.up,
            Knockback = 0f,
            IsCritical = false
        };
    }
}
