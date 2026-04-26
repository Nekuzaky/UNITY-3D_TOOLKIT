using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Guarantees a GameObject persists. Drop in once to avoid the boilerplate code.</summary>
    public sealed class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            if (transform.parent == null) DontDestroyOnLoad(gameObject);
        }
    }
}
