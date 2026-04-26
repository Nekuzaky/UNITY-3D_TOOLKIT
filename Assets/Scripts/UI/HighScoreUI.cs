using UnityEngine;
using TMPro;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    /// <summary>Displays the persisted high score.</summary>
    public sealed class HighScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "Best: {0}";

        private void OnEnable() { Refresh(); }

        public void Refresh()
        {
            if (_label == null) return;
            int hs = ScoreManager.HasInstance ? ScoreManager.Instance.HighScore : PlayerPrefs.GetInt("HighScore", 0);
            _label.text = string.Format(_format, hs);
        }
    }
}
