using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Combat
{
    /// <summary>Ranged weapon. Fires a projectile from the ObjectPool (R3).</summary>
    public sealed class RangedWeapon : WeaponBase
    {
        [SerializeField] private string _projectilePoolKey = "Bullet";
        [SerializeField] private float _projectileSpeed = 25f;
        [SerializeField] private float _projectileLifetime = 4f;

        protected override void FireInternal()
        {
            if (!ObjectPool.HasInstance) return; // R7
            var go = ObjectPool.Instance.Get(_projectilePoolKey, _muzzle.position, _muzzle.rotation);
            if (go == null) return;
            var projectile = go.GetComponent<ProjectileBase>();
            if (projectile == null) return;
            projectile.Launch(new ProjectileLaunchInfo
            {
                Direction = _muzzle.forward,
                Speed = _projectileSpeed,
                Lifetime = _projectileLifetime,
                Damage = _damage,
                DamageType = _damageType,
                Owner = _owner,
                PoolKey = _projectilePoolKey
            });
        }
    }
}
