using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Spawning
{
    /// <summary>Spawns a number of pooled objects in a box volume at a given moment.</summary>
    public sealed class AreaSpawner : MonoBehaviour
    {
        [SerializeField] private string _poolKey;
        [SerializeField] private Vector3 _size = new Vector3(10f, 0f, 10f);
        [Min(1)] [SerializeField] private int _count = 10;

        public void SpawnAll()
        {
            Debug.Assert(!string.IsNullOrEmpty(_poolKey), "[AreaSpawner.SpawnAll] _poolKey is empty"); // R5
            if (!ObjectPool.HasInstance) return;
            // R2: bounded by _count
            for (int i = 0; i < _count; i++)
            {
                Vector3 p = transform.position + new Vector3(
                    Random.Range(-_size.x * 0.5f, _size.x * 0.5f),
                    Random.Range(0f, _size.y),
                    Random.Range(-_size.z * 0.5f, _size.z * 0.5f));
                ObjectPool.Instance.Get(_poolKey, p, transform.rotation);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
            Gizmos.DrawWireCube(transform.position + Vector3.up * (_size.y * 0.5f), _size);
        }
    }
}
