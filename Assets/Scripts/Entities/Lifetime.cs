using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Entities
{
    /// <summary>Auto-despawn after a duration. Pool-friendly.</summary>
    public sealed class Lifetime : MonoBehaviour
    {
        [Min(0f)] [SerializeField] private float _seconds = 5f;
        [SerializeField] private string _poolKey;

        private float _despawnAt;

        private void OnEnable() { _despawnAt = Time.time + _seconds; }

        private void Update()
        {
            if (Time.time < _despawnAt) return;
            if (!string.IsNullOrEmpty(_poolKey) && ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else gameObject.SetActive(false);
        }
    }
}
