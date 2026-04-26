using UnityEngine;

namespace GameJamToolkit.Combat
{
    public struct ProjectileLaunchInfo
    {
        public Vector3 Direction;
        public float Speed;
        public float Lifetime;
        public float Damage;
        public DamageType DamageType;
        public GameObject Owner;
        public string PoolKey;
    }
}
