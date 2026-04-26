using UnityEngine;
using TMPro;

namespace GameJamToolkit.Racing
{
    /// <summary>HUD widget showing lap count + best lap time for one tracker.</summary>
    public sealed class RaceUI : MonoBehaviour
    {
        [SerializeField] private LapTracker _tracker;
        [SerializeField] private TMP_Text _lapLabel;
        [SerializeField] private TMP_Text _bestLapLabel;
        [SerializeField] private string _lapFormat = "Lap {0}";
        [SerializeField] private string _bestFormat = "Best {0:00}:{1:00.00}";

        private void Update()
        {
            if (_tracker == null) return;
            if (_lapLabel != null) _lapLabel.text = string.Format(_lapFormat, _tracker.CurrentLap + 1);
            if (_bestLapLabel != null && _tracker.BestLapSeconds > 0f)
            {
                int m = Mathf.FloorToInt(_tracker.BestLapSeconds / 60f);
                float s = _tracker.BestLapSeconds % 60f;
                _bestLapLabel.text = string.Format(_bestFormat, m, s);
            }
        }
    }
}
