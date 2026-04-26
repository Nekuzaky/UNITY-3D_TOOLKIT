using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Singleton that survives scene changes as DontDestroyOnLoad. Destroys
    /// duplicates spawned by a new scene.
    /// </summary>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (transform.parent != null) transform.SetParent(null); // R7: DontDestroyOnLoad requires a root GameObject
            base.Awake();
            if (this != null && gameObject != null) DontDestroyOnLoad(gameObject);
        }
    }
}
