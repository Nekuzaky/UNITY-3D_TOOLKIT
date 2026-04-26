using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Low-level cooldown based on Time.time. No allocation, no event.</summary>
    public struct Cooldown
    {
        public float Duration;
        public float NextReadyAt;

        public static Cooldown Of(float duration) => new Cooldown { Duration = duration, NextReadyAt = 0f };

        public bool IsReady => Time.time >= NextReadyAt;
        public float Remaining => Mathf.Max(0f, NextReadyAt - Time.time);

        public bool TryConsume()
        {
            if (!IsReady) return false;
            NextReadyAt = Time.time + Duration;
            return true;
        }

        public void Reset() { NextReadyAt = Time.time + Duration; }
        public void ForceReady() { NextReadyAt = 0f; }
    }
}
