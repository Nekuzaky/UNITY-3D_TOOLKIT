using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Spawning
{
    /// <summary>Periodically spawns the given prefab from the named pool.</summary>
    public sealed class TimedSpawner : MonoBehaviour
    {
        [SerializeField] private string _poolKey = "Enemy";
        [Min(0f)] [SerializeField] private float _interval = 2f;
        [Min(0)] [SerializeField] private int _maxConcurrent = 10;
        [Min(0f)] [SerializeField] private float _radius = 0f;

        private float _nextAt;
        private int _currentCount;

        private void Update()
        {
            if (Time.time < _nextAt) return;
            if (_currentCount >= _maxConcurrent) return;
            SpawnOne();
            _nextAt = Time.time + _interval;
        }

        private void SpawnOne()
        {
            if (!ObjectPool.HasInstance) return;
            Vector3 pos = transform.position;
            if (_radius > 0f)
            {
                Vector2 rnd = Random.insideUnitCircle * _radius;
                pos += new Vector3(rnd.x, 0f, rnd.y);
            }
            var go = ObjectPool.Instance.Get(_poolKey, pos, transform.rotation);
            if (go == null) return;
            _currentCount++;
            // simple lifetime tracking: observe SetActive
            var releaser = go.GetComponent<DespawnNotifier>();
            if (releaser == null) releaser = go.AddComponent<DespawnNotifier>();
            releaser.OnReleased = HandleReleased;
        }

        private void HandleReleased() { _currentCount = Mathf.Max(0, _currentCount - 1); }
    }
}
