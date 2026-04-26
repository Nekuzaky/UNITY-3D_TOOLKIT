using System;
using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>
    /// Generic timer. Tick is called manually (Update / FixedUpdate)
    /// to stay deterministic and avoid a MonoBehaviour dependency.
    /// </summary>
    public sealed class Timer
    {
        private float _duration;
        private float _remaining;
        private bool _isRunning;

        public bool IsRunning => _isRunning;
        public bool HasFinished => !_isRunning && _remaining <= 0f;
        public float Remaining => _remaining;
        public float Progress => _duration > 0f ? 1f - (_remaining / _duration) : 1f;
        public event Action OnComplete;

        public void Start(float seconds)
        {
            Debug.Assert(seconds >= 0f, "[Timer.Start] seconds is negative"); // R5
            _duration = Mathf.Max(0f, seconds);
            _remaining = _duration;
            _isRunning = _duration > 0f;
            if (!_isRunning) OnComplete?.Invoke();
        }

        public void Stop()
        {
            _isRunning = false;
            _remaining = 0f;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;
            _remaining -= deltaTime;
            if (_remaining > 0f) return;
            _remaining = 0f;
            _isRunning = false;
            OnComplete?.Invoke();
        }
    }
}
