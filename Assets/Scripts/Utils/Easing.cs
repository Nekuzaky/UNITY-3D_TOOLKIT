using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Common easing functions. t must be in [0,1].</summary>
    public static class Easing
    {
        public static float Linear(float t) => t;
        public static float QuadIn(float t) => t * t;
        public static float QuadOut(float t) => 1f - (1f - t) * (1f - t);
        public static float QuadInOut(float t) => t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) * 0.5f;
        public static float CubicIn(float t) => t * t * t;
        public static float CubicOut(float t) => 1f - Mathf.Pow(1f - t, 3f);
        public static float SineIn(float t) => 1f - Mathf.Cos((t * Mathf.PI) * 0.5f);
        public static float SineOut(float t) => Mathf.Sin((t * Mathf.PI) * 0.5f);
        public static float ExpoOut(float t) => Mathf.Approximately(t, 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * t);
        public static float BackIn(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            return c3 * t * t * t - c1 * t * t;
        }
        public static float BackOut(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            float p = t - 1f;
            return 1f + c3 * p * p * p + c1 * p * p;
        }
    }
}
