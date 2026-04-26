using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.VFX
{
    /// <summary>Static helper to spawn a pooled VFX and return it after _lifetime.</summary>
    public static class VFXSpawner
    {
        public static GameObject Spawn(string poolKey, Vector3 position, Quaternion rotation)
        {
            Debug.Assert(!string.IsNullOrEmpty(poolKey), "[VFXSpawner.Spawn] poolKey is empty"); // R5
            if (!ObjectPool.HasInstance) return null;
            return ObjectPool.Instance.Get(poolKey, position, rotation);
        }
    }
}
