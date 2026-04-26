using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.UI
{
    /// <summary>Displays the current score. Supports TMP_Text or legacy Unity Text.</summary>
    public sealed class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpLabel;
        [SerializeField] private Text _legacyLabel;
        [SerializeField] private string _format = "Score: {0}";

        private void OnEnable() { EventBus.Subscribe<ScoreChangedEvent>(HandleScore); RefreshFromManager(); }
        private void OnDisable() { EventBus.Unsubscribe<ScoreChangedEvent>(HandleScore); }

        private void RefreshFromManager()
        {
            if (!ScoreManager.HasInstance) return;
            HandleScore(new ScoreChangedEvent { Current = ScoreManager.Instance.CurrentScore });
        }

        private void HandleScore(ScoreChangedEvent evt)
        {
            string text = string.Format(_format, evt.Current);
            if (_tmpLabel != null) _tmpLabel.text = text;
            if (_legacyLabel != null) _legacyLabel.text = text;
        }
    }
}
