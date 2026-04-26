using UnityEngine;

namespace GameJamToolkit.Data
{
    /// <summary>
    /// Simple PlayerPrefs storage: stars + best time + completion.
    /// Sufficient for jams.
    /// </summary>
    public static class LevelProgress
    {
        public static int GetStars(string levelId) => PlayerPrefs.GetInt($"Lvl_{levelId}_Stars", 0);
        public static void SetStars(string levelId, int stars)
        {
            int clamped = Mathf.Clamp(stars, 0, 3);
            int previous = GetStars(levelId);
            if (clamped > previous) PlayerPrefs.SetInt($"Lvl_{levelId}_Stars", clamped);
        }

        public static float GetBestTime(string levelId) => PlayerPrefs.GetFloat($"Lvl_{levelId}_BestTime", -1f);
        public static void SetBestTime(string levelId, float seconds)
        {
            float prev = GetBestTime(levelId);
            if (prev < 0f || seconds < prev) PlayerPrefs.SetFloat($"Lvl_{levelId}_BestTime", seconds);
        }

        public static bool IsCompleted(string levelId) => PlayerPrefs.GetInt($"Lvl_{levelId}_Done", 0) == 1;
        public static void MarkCompleted(string levelId) { PlayerPrefs.SetInt($"Lvl_{levelId}_Done", 1); }
    }
}
