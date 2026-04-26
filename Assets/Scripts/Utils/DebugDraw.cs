using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Debug helper: draws lines to visualize runtime state.</summary>
    public static class DebugDraw
    {
        public static void Sphere(Vector3 center, float radius, Color color, float duration = 0f, int segments = 24)
        {
            Debug.Assert(radius >= 0f, "[DebugDraw.Sphere] radius is negative"); // R5
            Debug.Assert(segments >= 4, "[DebugDraw.Sphere] segments < 4"); // R5
            float step = 360f / segments;
            // R2 borne fixe = segments
            for (int i = 0; i < segments; i++)
            {
                float a0 = step * i * Mathf.Deg2Rad;
                float a1 = step * (i + 1) * Mathf.Deg2Rad;
                Vector3 p0 = center + new Vector3(Mathf.Cos(a0) * radius, 0f, Mathf.Sin(a0) * radius);
                Vector3 p1 = center + new Vector3(Mathf.Cos(a1) * radius, 0f, Mathf.Sin(a1) * radius);
                Debug.DrawLine(p0, p1, color, duration);
            }
        }

        public static void Cross(Vector3 center, float size, Color color, float duration = 0f)
        {
            Debug.DrawLine(center + Vector3.left * size, center + Vector3.right * size, color, duration);
            Debug.DrawLine(center + Vector3.up * size, center + Vector3.down * size, color, duration);
            Debug.DrawLine(center + Vector3.forward * size, center + Vector3.back * size, color, duration);
        }
    }
}
