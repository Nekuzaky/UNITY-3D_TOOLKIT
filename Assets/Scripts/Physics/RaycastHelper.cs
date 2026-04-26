using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Simplified Physics.Raycast wrappers for common use cases.</summary>
    public static class RaycastHelper
    {
        public static bool Forward(Transform from, float distance, LayerMask mask, out RaycastHit hit)
        {
            Debug.Assert(from != null, "[RaycastHelper.Forward] from null"); // R5
            hit = default;
            return from != null && Physics.Raycast(from.position, from.forward, out hit, distance, mask, QueryTriggerInteraction.Ignore);
        }

        public static bool ToTarget(Vector3 from, Vector3 target, LayerMask mask, out RaycastHit hit)
        {
            Vector3 dir = target - from;
            return Physics.Raycast(from, dir.normalized, out hit, dir.magnitude, mask, QueryTriggerInteraction.Ignore);
        }
    }
}
