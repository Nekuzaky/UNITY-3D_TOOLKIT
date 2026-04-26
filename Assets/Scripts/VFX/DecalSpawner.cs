using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.VFX
{
    /// <summary>Helper: places a pooled decal at a raycast hit (bullet impact).</summary>
    public static class DecalSpawner
    {
        public static GameObject Spawn(string poolKey, RaycastHit hit, float yOffset = 0.01f)
        {
            Debug.Assert(!string.IsNullOrEmpty(poolKey), "[DecalSpawner.Spawn] poolKey is empty"); // R5
            if (!ObjectPool.HasInstance) return null;
            Vector3 pos = hit.point + hit.normal * yOffset;
            Quaternion rot = Quaternion.LookRotation(-hit.normal);
            return ObjectPool.Instance.Get(poolKey, pos, rot);
        }
    }
}
