using UnityEngine;
using TMPro;

namespace GameJamToolkit.Misc
{
    /// <summary>Shows FPS, draw calls, allocated memory. For runtime debug.</summary>
    public sealed class PerformanceOverlay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _refreshInterval = 0.5f;

        private float _nextRefresh;
        private float _frameAccum;
        private int _frameCount;

        private void Update()
        {
            _frameAccum += Time.unscaledDeltaTime;
            _frameCount++;
            if (Time.unscaledTime < _nextRefresh) return;
            _nextRefresh = Time.unscaledTime + _refreshInterval;
            float fps = _frameAccum > 0f ? _frameCount / _frameAccum : 0f;
            long mem = System.GC.GetTotalMemory(false) / (1024 * 1024);
            if (_label != null) _label.text = $"FPS {fps:0.0}  Mem {mem} MB";
            _frameAccum = 0f;
            _frameCount = 0;
        }
    }
}
