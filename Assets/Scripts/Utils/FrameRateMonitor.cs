using UnityEngine;
using TMPro;

namespace GameJamToolkit.Utils
{
    /// <summary>Shows a smoothed FPS counter. For debug; hide in production builds.</summary>
    public sealed class FrameRateMonitor : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _smoothing = 0.5f;

        private float _smoothFps;

        private void Update()
        {
            float instant = 1f / Mathf.Max(0.0001f, Time.unscaledDeltaTime);
            _smoothFps = Mathf.Lerp(_smoothFps, instant, _smoothing * Time.unscaledDeltaTime);
            if (_label != null) _label.text = $"FPS: {Mathf.RoundToInt(_smoothFps)}";
        }
    }
}
