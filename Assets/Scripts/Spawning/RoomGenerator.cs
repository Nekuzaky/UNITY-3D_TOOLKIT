using UnityEngine;

namespace GameJamToolkit.Spawning
{
    /// <summary>
    /// Very simple 1D "room" generator (corridors). Chains n prefabs
    /// in a given direction. For basic jam roguelikes.
    /// </summary>
    public sealed class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _roomPrefabs;
        [Min(1)] [SerializeField] private int _count = 10;
        [SerializeField] private float _stepDistance = 12f;
        [SerializeField] private Vector3 _stepDirection = Vector3.forward;

        private void Start() { Generate(); }

        public void Generate()
        {
            Debug.Assert(_roomPrefabs != null && _roomPrefabs.Length > 0, "[RoomGenerator.Generate] _roomPrefabs is empty"); // R5
            if (_roomPrefabs == null || _roomPrefabs.Length == 0) return;
            Vector3 cursor = transform.position;
            // R2: bounded by _count
            for (int i = 0; i < _count; i++)
            {
                var prefab = _roomPrefabs[Random.Range(0, _roomPrefabs.Length)];
                Instantiate(prefab, cursor, Quaternion.identity, transform);
                cursor += _stepDirection.normalized * _stepDistance;
            }
        }
    }
}
