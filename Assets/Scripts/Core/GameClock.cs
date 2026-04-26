using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Game clock. Independent of Time.timeScale: lets you pause gameplay
    /// without pausing UI (menus, transitions).
    /// </summary>
    public sealed class GameClock : Singleton<GameClock>
    {
        public float ElapsedSeconds { get; private set; }
        public bool IsRunning { get; private set; }

        public void StartClock() { IsRunning = true; }
        public void StopClock() { IsRunning = false; }
        public void ResetClock() { ElapsedSeconds = 0f; }

        private void Update()
        {
            if (!IsRunning) return;
            ElapsedSeconds += Time.unscaledDeltaTime;
        }
    }
}
