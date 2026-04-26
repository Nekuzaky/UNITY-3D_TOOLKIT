using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Implemented by any object that can take damage.</summary>
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(DamageInfo info);
        GameObject GameObject { get; }
    }
}
