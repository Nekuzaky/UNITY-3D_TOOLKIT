using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Utils
{
    /// <summary>Destroys or returns to the pool after a delay. R3-friendly.</summary>
    public sealed class AutoDestroy : MonoBehaviour
    {
        [Min(0f)] [SerializeField] private float _delay = 2f;
        [SerializeField] private string _poolKey;

        private float _despawnAt;

        private void OnEnable() { _despawnAt = Time.time + _delay; }

        private void Update()
        {
            if (Time.time < _despawnAt) return;
            if (!string.IsNullOrEmpty(_poolKey) && ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else Destroy(gameObject);
        }
    }
}
