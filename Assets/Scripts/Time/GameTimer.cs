using System;
using UnityEngine;

namespace GameJamToolkit.TimeSystem
{
    /// <summary>Countdown timer. Notifies OnFinished at the end.</summary>
    public sealed class GameTimer : MonoBehaviour
    {
        [SerializeField] private float _seconds = 60f;
        [SerializeField] private bool _autoStart = true;

        public float Remaining { get; private set; }
        public bool IsRunning { get; private set; }
        public event Action OnFinished;

        private void Start() { Remaining = _seconds; if (_autoStart) IsRunning = true; }

        private void Update()
        {
            if (!IsRunning) return;
            Remaining -= Time.deltaTime;
            if (Remaining > 0f) return;
            Remaining = 0f;
            IsRunning = false;
            OnFinished?.Invoke();
        }

        public void StartTimer() { Remaining = _seconds; IsRunning = true; }
        public void Stop() { IsRunning = false; }
        public void AddTime(float s) { Remaining = Mathf.Max(0f, Remaining + s); }
    }
}
