using System.Collections;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Entities
{
    /// <summary>Plays a sequence of waves from a WaveConfig.</summary>
    public sealed class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private WaveConfig[] _waves;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _target;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _autoStart = true;

        public int CurrentWaveIndex { get; private set; } = -1;
        public event System.Action<int> OnWaveStarted;
        public event System.Action OnAllWavesCompleted;

        private Coroutine _routine;

        private void Start() { if (_autoStart) StartWaves(); }

        public void StartWaves()
        {
            Debug.Assert(_waves != null && _waves.Length > 0, "[WaveSpawner.StartWaves] no waves"); // R5
            Debug.Assert(_spawnPoints != null && _spawnPoints.Length > 0, "[WaveSpawner.StartWaves] no spawn points"); // R5
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(RunRoutine());
        }

        private IEnumerator RunRoutine()
        {
            int safety = 0; // R2: hard stop guard against runaway loops
            do
            {
                int max = _waves.Length;
                for (int i = 0; i < max; i++)
                {
                    var wave = _waves[i];
                    if (wave == null) continue;
                    CurrentWaveIndex = i;
                    OnWaveStarted?.Invoke(i);
                    yield return new WaitForSeconds(wave.DelayBeforeWave);
                    yield return SpawnWave(wave);
                    yield return new WaitForSeconds(wave.DelayAfterWave);
                }
                OnAllWavesCompleted?.Invoke();
                safety++;
                if (safety > 999) yield break; // R2 cap absolu
            } while (_loop);
        }

        private IEnumerator SpawnWave(WaveConfig wave)
        {
            int eMax = wave.Entries != null ? wave.Entries.Length : 0;
            for (int i = 0; i < eMax; i++)
            {
                var entry = wave.Entries[i];
                if (entry == null) continue;
                yield return new WaitForSeconds(entry.StartDelay);
                int n = entry.Count;
                for (int k = 0; k < n; k++)
                {
                    SpawnAt(entry.PoolKey);
                    yield return new WaitForSeconds(entry.SpawnInterval);
                }
            }
        }

        private void SpawnAt(string key)
        {
            if (!ObjectPool.HasInstance || _spawnPoints == null || _spawnPoints.Length == 0) return;
            var sp = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            var go = ObjectPool.Instance.Get(key, sp.position, sp.rotation);
            if (go == null) return;
            var enemy = go.GetComponent<EnemyBase>();
            if (enemy != null && _target != null) enemy.SetTarget(_target);
        }
    }
}
