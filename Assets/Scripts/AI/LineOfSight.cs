using UnityEngine;

namespace GameJamToolkit.AI
{
    /// <summary>Simple line-of-sight test between self and target. Public static utility.</summary>
    public static class LineOfSight
    {
        public static bool Has(Vector3 from, Vector3 to, LayerMask obstacleMask)
        {
            Vector3 dir = to - from;
            float dist = dir.magnitude;
            if (dist < 0.001f) return true;
            return !Physics.Raycast(from, dir.normalized, dist, obstacleMask, QueryTriggerInteraction.Ignore);
        }
    }
}
