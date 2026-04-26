using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Spawning
{
    /// <summary>Spawns once when an object (player) enters the trigger.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerSpawner : MonoBehaviour
    {
        [SerializeField] private string _poolKey;
        [Min(1)] [SerializeField] private int _count = 1;
        [SerializeField] private float _spread = 1.5f;
        [SerializeField] private string _filterTag = "Player";

        private bool _consumed;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (_consumed) return;
            if (!string.IsNullOrEmpty(_filterTag) && !other.CompareTag(_filterTag)) return;
            if (!ObjectPool.HasInstance) return;
            // R2: bounded by _count
            for (int i = 0; i < _count; i++)
            {
                Vector2 r = Random.insideUnitCircle * _spread;
                Vector3 pos = transform.position + new Vector3(r.x, 0f, r.y);
                ObjectPool.Instance.Get(_poolKey, pos, transform.rotation);
            }
            _consumed = true;
        }
    }
}
