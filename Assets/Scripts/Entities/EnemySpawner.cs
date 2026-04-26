using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Entities
{
    /// <summary>Spawns an enemy via the ObjectPool at a given point.</summary>
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private string _enemyPoolKey = "Enemy";
        [Min(0f)] [SerializeField] private float _initialDelay = 0f;
        [Min(0f)] [SerializeField] private float _interval = 5f;
        [Min(1)] [SerializeField] private int _maxAlive = 5;
        [SerializeField] private Transform _target;

        private float _nextSpawnAt;
        private int _aliveCount;

        private void Start()
        {
            _nextSpawnAt = Time.time + _initialDelay;
        }

        private void Update()
        {
            if (Time.time < _nextSpawnAt) return;
            if (_aliveCount >= _maxAlive) return;
            SpawnOne();
            _nextSpawnAt = Time.time + _interval;
        }

        private void SpawnOne()
        {
            if (!ObjectPool.HasInstance) return;
            var go = ObjectPool.Instance.Get(_enemyPoolKey, transform.position, transform.rotation);
            if (go == null) return;
            _aliveCount++;
            var enemy = go.GetComponent<EnemyBase>();
            if (enemy != null) enemy.SetTarget(_target);
            var health = go.GetComponent<Combat.HealthComponent>();
            if (health != null) health.OnDied += DecrementCounter;
        }

        private void DecrementCounter() { _aliveCount = Mathf.Max(0, _aliveCount - 1); }
    }
}
