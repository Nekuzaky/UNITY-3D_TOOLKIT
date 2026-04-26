using System;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Rhythm
{
    /// <summary>Spawns rhythm notes a few beats ahead of their target.</summary>
    public sealed class NoteSpawner : MonoBehaviour
    {
        [Serializable] public class NoteEvent { public float Beat; public string Lane; }
        [SerializeField] private BeatClock _clock;
        [SerializeField] private NoteEvent[] _events;
        [SerializeField] private string _notePoolKey = "Note";
        [SerializeField] private int _spawnLeadBeats = 4;

        private int _nextIndex;

        private void Update()
        {
            if (_clock == null || _events == null || _nextIndex >= _events.Length) return;
            int spawnAt = _clock.CurrentBeat + _spawnLeadBeats;
            while (_nextIndex < _events.Length && _events[_nextIndex].Beat <= spawnAt)
            {
                SpawnNote(_events[_nextIndex]);
                _nextIndex++;
            }
        }

        private void SpawnNote(NoteEvent evt)
        {
            if (!ObjectPool.HasInstance) return;
            ObjectPool.Instance.Get(_notePoolKey, transform.position, transform.rotation);
        }
    }
}
