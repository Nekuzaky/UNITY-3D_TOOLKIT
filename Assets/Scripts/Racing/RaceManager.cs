using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Racing
{
    /// <summary>
    /// Wires checkpoints to lap trackers. Hub for the race state. Publishes
    /// position updates and end-of-race results.
    /// </summary>
    public sealed class RaceManager : MonoBehaviour
    {
        [SerializeField] private List<RaceCheckpoint> _checkpointList = new List<RaceCheckpoint>();
        [SerializeField] private List<LapTracker> _trackerList = new List<LapTracker>();

        public event System.Action<LapTracker> OnParticipantFinished;
        public event System.Action OnRaceComplete;

        private int _finishedCount;

        private void Start()
        {
            int max = _checkpointList.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var cp = _checkpointList[i];
                if (cp == null) continue;
                cp.OnReached += BroadcastCheckpoint;
            }

            int t = _trackerList.Count; // R2
            for (int i = 0; i < t; i++)
            {
                var tracker = _trackerList[i];
                if (tracker == null) continue;
                tracker.OnRaceFinished += () => HandleParticipantFinished(tracker);
            }
        }

        private void BroadcastCheckpoint(int index, GameObject who)
        {
            int max = _trackerList.Count;
            for (int i = 0; i < max; i++)
            {
                var t = _trackerList[i];
                if (t != null) t.HandleCheckpoint(index, who);
            }
        }

        private void HandleParticipantFinished(LapTracker tracker)
        {
            OnParticipantFinished?.Invoke(tracker);
            _finishedCount++;
            if (_finishedCount >= _trackerList.Count) OnRaceComplete?.Invoke();
        }
    }
}
