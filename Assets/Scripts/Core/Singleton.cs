using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Generic MonoBehaviour singleton. The first instance found in the scene wins;
    /// any subsequent instance is destroyed. For a singleton that persists across
    /// scenes, use <see cref="PersistentSingleton{T}"/>.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();
        private static bool _isQuitting;

        public static T Instance
        {
            get
            {
                if (_isQuitting) return null;
                lock (_lock)
                {
                    if (_instance != null) return _instance;
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " (Auto)");
                        _instance = go.AddComponent<T>();
                    }
                    return _instance;
                }
            }
        }

        public static bool HasInstance => _instance != null && !_isQuitting;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}
