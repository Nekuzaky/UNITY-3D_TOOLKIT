using UnityEngine;

namespace GameJamToolkit.Utils
{
    public struct Stopwatch
    {
        public float StartTime;
        public bool IsRunning;

        public static Stopwatch StartNew() => new Stopwatch { StartTime = Time.realtimeSinceStartup, IsRunning = true };

        public float Elapsed => IsRunning ? Time.realtimeSinceStartup - StartTime : 0f;

        public void Stop() { IsRunning = false; }
    }
}
