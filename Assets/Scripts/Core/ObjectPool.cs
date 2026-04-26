using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Generic object pool. Any gameplay logic must use this instead of
    /// Instantiate / Destroy (R3). Pools are indexed by string key.
    /// </summary>
    [DefaultExecutionOrder(-90)]
    public sealed class ObjectPool : PersistentSingleton<ObjectPool>
    {
        [SerializeField] private List<PoolEntry> _entryList = new List<PoolEntry>();

        private readonly Dictionary<string, Queue<GameObject>> _poolDict = new Dictionary<string, Queue<GameObject>>();
        private readonly Dictionary<string, GameObject> _prefabDict = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, int> _maxSizeDict = new Dictionary<string, int>();
        private readonly Dictionary<string, Transform> _rootDict = new Dictionary<string, Transform>();

        protected override void Awake()
        {
            base.Awake();
            PrewarmAll();
        }

        private void PrewarmAll()
        {
            // R2: fixed bound = list length
            int count = _entryList.Count;
            for (int i = 0; i < count; i++)
            {
                var entry = _entryList[i];
                if (entry == null || entry.Prefab == null || string.IsNullOrEmpty(entry.Key)) continue;
                RegisterPool(entry.Key, entry.Prefab, entry.InitialSize, entry.MaxSize);
            }
        }

        /// <summary>Register a pool at runtime (e.g. prefabs loaded from a ScriptableObject).</summary>
        public void RegisterPool(string key, GameObject prefab, int initialSize, int maxSize)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "[ObjectPool.RegisterPool] key is empty"); // R5
            Debug.Assert(prefab != null, "[ObjectPool.RegisterPool] prefab is null"); // R5
            if (string.IsNullOrEmpty(key) || prefab == null) return;
            if (_poolDict.ContainsKey(key)) return;

            _prefabDict[key] = prefab;
            _maxSizeDict[key] = Mathf.Max(1, maxSize);
            var root = new GameObject($"Pool_{key}").transform;
            root.SetParent(transform);
            _rootDict[key] = root;

            var queue = new Queue<GameObject>(initialSize);
            // R2: fixed bound initialSize
            for (int i = 0; i < initialSize; i++)
            {
                var go = CreateInstance(key, prefab, root);
                go.SetActive(false);
                queue.Enqueue(go);
            }
            _poolDict[key] = queue;
        }

        /// <summary>Get an object from the pool. Creates a new instance if empty and MaxSize is not reached.</summary>
        public GameObject Get(string key, Vector3 position, Quaternion rotation)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "[ObjectPool.Get] key is empty"); // R5
            if (!_poolDict.TryGetValue(key, out var queue))
            {
                Debug.LogWarning($"[ObjectPool] unknown key '{key}'");
                return null;
            }

            GameObject go;
            if (queue.Count > 0)
            {
                go = queue.Dequeue();
            }
            else
            {
                go = CreateInstance(key, _prefabDict[key], _rootDict[key]);
            }
            if (go == null) return null;

            var t = go.transform;
            t.SetPositionAndRotation(position, rotation);
            go.SetActive(true);
            return go;
        }

        /// <summary>Return an object to the pool.</summary>
        public void Return(string key, GameObject go)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "[ObjectPool.Return] key is empty"); // R5
            Debug.Assert(go != null, "[ObjectPool.Return] go is null"); // R5
            if (go == null) return;

            if (!_poolDict.TryGetValue(key, out var queue))
            {
                Destroy(go);
                return;
            }

            int max = _maxSizeDict.TryGetValue(key, out var m) ? m : 256;
            if (queue.Count >= max)
            {
                Destroy(go); // R3 exempt: pool saturated, free the memory
                return;
            }

            go.SetActive(false);
            if (_rootDict.TryGetValue(key, out var root)) go.transform.SetParent(root);
            queue.Enqueue(go);
        }

        private GameObject CreateInstance(string key, GameObject prefab, Transform parent)
        {
            var go = Instantiate(prefab, parent); // R3 exempt: pool creation
            var poolable = go.GetComponent<Poolable>();
            if (poolable == null) poolable = go.AddComponent<Poolable>();
            poolable.Setup(key);
            return go;
        }

        public bool HasPool(string key) => _poolDict.ContainsKey(key);
        public int CountAvailable(string key) => _poolDict.TryGetValue(key, out var q) ? q.Count : 0;
    }
}
