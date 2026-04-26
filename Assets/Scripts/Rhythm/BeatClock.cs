using System;
using UnityEngine;

namespace GameJamToolkit.Rhythm
{
    /// <summary>
    /// BPM-based beat clock. Drives rhythm games. Fires OnBeat at every beat
    /// boundary; expose the current beat phase 0..1 for visualizers.
    /// </summary>
    public sealed class BeatClock : MonoBehaviour
    {
        [Min(20f)] [SerializeField] private float _bpm = 120f;
        [SerializeField] private bool _autoStart = true;

        public bool IsRunning { get; private set; }
        public float Bpm => _bpm;
        public float BeatDuration => 60f / Mathf.Max(1f, _bpm);
        public int CurrentBeat { get; private set; }
        public float BeatPhase01 { get; private set; }
        public event Action<int> OnBeat;

        private float _startTime;

        private void Start() { if (_autoStart) StartClock(); }

        public void StartClock()
        {
            IsRunning = true;
            _startTime = Time.time;
            CurrentBeat = 0;
        }

        public void Stop() { IsRunning = false; }

        private void Update()
        {
            if (!IsRunning) return;
            float elapsed = Time.time - _startTime;
            int beat = Mathf.FloorToInt(elapsed / BeatDuration);
            BeatPhase01 = Mathf.Clamp01((elapsed % BeatDuration) / BeatDuration);
            if (beat <= CurrentBeat) return;
            CurrentBeat = beat;
            OnBeat?.Invoke(CurrentBeat);
        }
    }
}
