using UnityEngine;

namespace GameJamToolkit.Spawning
{
    /// <summary>Randomly scatters prefabs inside a circle (debris, grass, decor).</summary>
    public sealed class RandomScatter : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        [Min(1)] [SerializeField] private int _count = 30;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private bool _alignToGround = true;
        [SerializeField] private LayerMask _groundMask = ~0;

        private void Start() { Scatter(); }

        public void Scatter()
        {
            Debug.Assert(_prefabs != null && _prefabs.Length > 0, "[RandomScatter.Scatter] _prefabs are empty"); // R5
            if (_prefabs == null || _prefabs.Length == 0) return;
            // R2: bounded by _count
            for (int i = 0; i < _count; i++)
            {
                var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
                Vector2 offset = Random.insideUnitCircle * _radius;
                Vector3 pos = transform.position + new Vector3(offset.x, 5f, offset.y);
                Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                if (_alignToGround && Physics.Raycast(pos, Vector3.down, out var hit, 50f, _groundMask, QueryTriggerInteraction.Ignore))
                {
                    pos.y = hit.point.y;
                }
                else pos.y = transform.position.y;
                Instantiate(prefab, pos, rot, transform); // R3 exempt: generation init
            }
        }
    }
}
