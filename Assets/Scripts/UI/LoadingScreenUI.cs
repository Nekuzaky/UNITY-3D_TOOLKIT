using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.UI
{
    /// <summary>Shows a progress slider during scene loading.</summary>
    public sealed class LoadingScreenUI : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "Loading... {0}%";

        private void OnEnable() { EventBus.Subscribe<SceneLoadStartedEvent>(HandleStarted); EventBus.Subscribe<SceneLoadCompletedEvent>(HandleCompleted); }
        private void OnDisable() { EventBus.Unsubscribe<SceneLoadStartedEvent>(HandleStarted); EventBus.Unsubscribe<SceneLoadCompletedEvent>(HandleCompleted); }

        private void HandleStarted(SceneLoadStartedEvent evt) { gameObject.SetActive(true); }
        private void HandleCompleted(SceneLoadCompletedEvent evt) { gameObject.SetActive(false); }

        private void Update()
        {
            if (!SceneLoader.HasInstance || !SceneLoader.Instance.IsLoading) return;
            float p = SceneLoader.Instance.Progress;
            if (_progressBar != null) _progressBar.value = p;
            if (_label != null) _label.text = string.Format(_format, Mathf.RoundToInt(p * 100f));
        }
    }
}
