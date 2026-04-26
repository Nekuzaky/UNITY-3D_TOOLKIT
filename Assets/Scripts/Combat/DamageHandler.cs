using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Static helper to apply damage to a GameObject. Centralizes
    /// the IDamageable lookup and DamageInfo construction.
    /// </summary>
    public static class DamageHandler
    {
        public static bool TryDamage(GameObject target, DamageInfo info)
        {
            Debug.Assert(target != null, "[DamageHandler.TryDamage] target is null"); // R5
            if (target == null) return false;

            var damageable = target.GetComponentInParent<IDamageable>();
            if (damageable == null) return false;
            damageable.TakeDamage(info);
            return true;
        }

        public static bool TryDamage(GameObject target, float amount, GameObject source)
        {
            Debug.Assert(target != null, "[DamageHandler.TryDamage] target is null"); // R5
            if (amount <= 0f) return false;
            return TryDamage(target, DamageInfo.Default(amount, source));
        }
    }
}
