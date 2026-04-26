using UnityEngine;

namespace GameJamToolkit.Spawning
{
    /// <summary>
    /// Generates an NxM grid of tiles from a prefab. Useful for
    /// simple procedural levels (jam).
    /// </summary>
    public sealed class TileGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [Min(1)] [SerializeField] private int _width = 10;
        [Min(1)] [SerializeField] private int _height = 10;
        [SerializeField] private float _spacing = 1f;

        private void Start() { Generate(); }

        public void Generate()
        {
            Debug.Assert(_tilePrefab != null, "[TileGenerator.Generate] _tilePrefab null"); // R5
            if (_tilePrefab == null) return;
            // R3 exempt: generation init de scene
            // R2 borne fixe = _width * _height
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    Vector3 p = transform.position + new Vector3(x * _spacing, 0f, z * _spacing);
                    Instantiate(_tilePrefab, p, transform.rotation, transform);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            Vector3 size = new Vector3(_width * _spacing, 0.1f, _height * _spacing);
            Vector3 center = transform.position + new Vector3((_width - 1) * _spacing * 0.5f, 0f, (_height - 1) * _spacing * 0.5f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
