using UnityEngine;
using TMPro;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    /// <summary>Displays the current play time (mm:ss) based on GameClock.</summary>
    public sealed class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "{0:00}:{1:00}";

        private void Update()
        {
            if (_label == null || !GameClock.HasInstance) return;
            float t = GameClock.Instance.ElapsedSeconds;
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            _label.text = string.Format(_format, minutes, seconds);
        }
    }
}
