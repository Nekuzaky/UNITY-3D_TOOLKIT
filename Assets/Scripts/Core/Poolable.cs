using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Attach to any prefab managed by <see cref="ObjectPool"/>. Stores the
    /// origin pool key for easy return-to-pool.
    /// </summary>
    public sealed class Poolable : MonoBehaviour
    {
        [SerializeField] private string _poolKey;
        [SerializeField] private float _autoReturnSeconds = -1f; // <=0 disables

        private float _returnAt;
        private bool _isActiveInPool;

        public string PoolKey => _poolKey;

        public void Setup(string poolKey)
        {
            _poolKey = poolKey;
        }

        private void OnEnable()
        {
            _isActiveInPool = true;
            if (_autoReturnSeconds > 0f) _returnAt = Time.time + _autoReturnSeconds;
        }

        private void Update()
        {
            if (!_isActiveInPool || _autoReturnSeconds <= 0f) return;
            if (Time.time >= _returnAt) ReturnToPool();
        }

        public void ReturnToPool()
        {
            Debug.Assert(!string.IsNullOrEmpty(_poolKey), "[Poolable] _poolKey is empty"); // R5
            if (string.IsNullOrEmpty(_poolKey)) { Destroy(gameObject); return; }
            if (!ObjectPool.HasInstance) { Destroy(gameObject); return; }
            _isActiveInPool = false;
            ObjectPool.Instance.Return(_poolKey, gameObject);
        }
    }
}
