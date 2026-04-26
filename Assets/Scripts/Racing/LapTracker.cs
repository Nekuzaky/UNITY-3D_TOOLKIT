using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Racing
{
    /// <summary>
    /// Tracks one participant: number of laps + checkpoint progress + best lap time.
    /// One LapTracker per car. Pair with a RaceManager that owns several.
    /// </summary>
    public sealed class LapTracker : MonoBehaviour
    {
        [SerializeField] private GameObject _participant;
        [SerializeField] private int _checkpointCount = 8;
        [SerializeField] private int _totalLaps = 3;

        public int CurrentLap { get; private set; }
        public int LastReachedIndex { get; private set; } = -1;
        public float CurrentLapStartTime { get; private set; }
        public float BestLapSeconds { get; private set; } = -1f;
        public bool RaceFinished { get; private set; }

        public event System.Action<int> OnLapCompleted;
        public event System.Action OnRaceFinished;

        public GameObject Participant => _participant;

        private void Awake() { CurrentLapStartTime = Time.time; }

        public void HandleCheckpoint(int index, GameObject who)
        {
            if (RaceFinished) return;
            if (_participant != null && who != _participant) return;

            int expected = (LastReachedIndex + 1) % Mathf.Max(1, _checkpointCount);
            if (index != expected) return; // R7: ordering enforced
            LastReachedIndex = index;

            if (index == _checkpointCount - 1) CompleteLap();
        }

        private void CompleteLap()
        {
            float lap = Time.time - CurrentLapStartTime;
            if (BestLapSeconds < 0f || lap < BestLapSeconds) BestLapSeconds = lap;
            CurrentLap++;
            LastReachedIndex = -1;
            CurrentLapStartTime = Time.time;
            OnLapCompleted?.Invoke(CurrentLap);
            if (CurrentLap >= _totalLaps)
            {
                RaceFinished = true;
                OnRaceFinished?.Invoke();
            }
        }
    }
}
