using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Common gameplay math operations (remap, snap, etc.).</summary>
    public static class MathHelper
    {
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            float t = Mathf.InverseLerp(fromMin, fromMax, value);
            return Mathf.Lerp(toMin, toMax, t);
        }

        public static float SnapTo(float value, float step)
        {
            Debug.Assert(step > 0f, "[MathHelper.SnapTo] step <= 0"); // R5
            if (step <= 0f) return value;
            return Mathf.Round(value / step) * step;
        }

        public static Vector3 SnapTo(Vector3 v, float step)
        {
            return new Vector3(SnapTo(v.x, step), SnapTo(v.y, step), SnapTo(v.z, step));
        }

        public static float SignedAngle(Vector3 a, Vector3 b, Vector3 axis)
        {
            return Vector3.SignedAngle(a, b, axis);
        }

        public static bool Approximately(Vector3 a, Vector3 b, float epsilon = 0.001f)
        {
            return (a - b).sqrMagnitude <= epsilon * epsilon;
        }

        public static float SmoothPing(float seconds, float frequency)
        {
            return (Mathf.Sin(seconds * frequency * Mathf.PI * 2f) + 1f) * 0.5f;
        }
    }
}
