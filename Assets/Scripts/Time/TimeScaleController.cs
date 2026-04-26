using UnityEngine;

namespace GameJamToolkit.TimeSystem
{
    /// <summary>Simple Time.timeScale control (slow-mo, bullet-time).</summary>
    public sealed class TimeScaleController : MonoBehaviour
    {
        public static void SlowDown(float scale) { Time.timeScale = Mathf.Clamp(scale, 0f, 1f); }
        public static void Restore() { Time.timeScale = 1f; }
        public static void Pause() { Time.timeScale = 0f; }
    }
}
