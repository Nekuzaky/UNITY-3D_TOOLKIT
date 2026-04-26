using UnityEngine;

namespace GameJamToolkit.Stealth
{
    /// <summary>
    /// Emits a noise at a position with a radius. Any NoiseListener in range
    /// reacts. Triggered by gameplay (footsteps, gunfire, drop).
    /// </summary>
    public static class NoiseEmitter
    {
        public static void Emit(Vector3 position, float radius, GameObject source = null)
        {
            Debug.Assert(radius > 0f, "[NoiseEmitter.Emit] radius <= 0"); // R5
            if (radius <= 0f) return;
            var hits = Physics.OverlapSphere(position, radius);
            int max = hits.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var listener = hits[i].GetComponentInParent<NoiseListener>();
                if (listener == null) continue;
                listener.OnNoise(position, source);
            }
        }
    }
}
