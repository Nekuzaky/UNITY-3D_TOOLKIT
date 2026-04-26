using UnityEngine;

namespace GameJamToolkit.Misc
{
    /// <summary>Logs prefixed by module + level. Centralizes project logs.</summary>
    public static class LogManager
    {
        public static bool LogVerbose = true;
        public static bool LogWarnings = true;

        public static void Log(string module, string message)
        {
            if (!LogVerbose) return;
            Debug.Log($"[{module}] {message}");
        }

        public static void Warn(string module, string message)
        {
            if (!LogWarnings) return;
            Debug.LogWarning($"[{module}] {message}");
        }

        public static void Error(string module, string message)
        {
            Debug.LogError($"[{module}] {message}");
        }
    }
}
