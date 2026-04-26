using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>Helper to quit the application (Editor + build compatible).</summary>
    public static class AppQuitter
    {
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
