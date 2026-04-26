using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Loads scenes asynchronously and broadcasts SceneLoadStarted /
    /// SceneLoadCompleted events. The only module allowed to call
    /// SceneManager.LoadScene directly.
    /// </summary>
    [DefaultExecutionOrder(-95)]
    public sealed class SceneLoader : PersistentSingleton<SceneLoader>
    {
        [SerializeField] private float _minLoadSeconds = 0.5f;

        public bool IsLoading { get; private set; }
        public float Progress { get; private set; }

        public void LoadScene(string sceneName)
        {
            Debug.Assert(!string.IsNullOrEmpty(sceneName), "[SceneLoader.LoadScene] sceneName is empty"); // R5
            Debug.Assert(!IsLoading, "[SceneLoader.LoadScene] already loading"); // R5
            if (string.IsNullOrEmpty(sceneName) || IsLoading) return;
            StartCoroutine(LoadRoutine(sceneName));
        }

        public void ReloadCurrent()
        {
            string name = SceneManager.GetActiveScene().name;
            LoadScene(name);
        }

        private IEnumerator LoadRoutine(string sceneName)
        {
            IsLoading = true;
            Progress = 0f;
            EventBus.Publish(new SceneLoadStartedEvent { SceneName = sceneName });

            float t0 = Time.realtimeSinceStartup;
            var op = SceneManager.LoadSceneAsync(sceneName);
            if (op == null) { IsLoading = false; yield break; } // R7
            op.allowSceneActivation = false;

            // R2: bounded by Unity's progress (never infinite)
            while (op.progress < 0.9f)
            {
                Progress = Mathf.Clamp01(op.progress / 0.9f);
                yield return null;
            }
            Progress = 1f;

            float elapsed = Time.realtimeSinceStartup - t0;
            float remain = _minLoadSeconds - elapsed;
            if (remain > 0f) yield return new WaitForSecondsRealtime(remain);

            op.allowSceneActivation = true;
            // R2: until the scene is fully activated
            while (!op.isDone) yield return null;

            IsLoading = false;
            EventBus.Publish(new SceneLoadCompletedEvent { SceneName = sceneName });
        }
    }
}
