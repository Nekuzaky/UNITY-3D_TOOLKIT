using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Spawning
{
    /// <summary>Spawns along a sequence of waypoints (corridors, rails).</summary>
    public sealed class PathSpawner : MonoBehaviour
    {
        [SerializeField] private string _poolKey;
        [SerializeField] private Transform[] _waypoints;
        [Min(0f)] [SerializeField] private float _spacing = 2f;

        public void SpawnAll()
        {
            Debug.Assert(_waypoints != null && _waypoints.Length >= 2, "[PathSpawner.SpawnAll] _waypoints insuffisants"); // R5
            if (!ObjectPool.HasInstance || _waypoints == null) return;
            int max = _waypoints.Length;
            for (int i = 0; i < max - 1; i++)
            {
                Vector3 a = _waypoints[i].position;
                Vector3 b = _waypoints[i + 1].position;
                float dist = Vector3.Distance(a, b);
                int n = Mathf.Max(1, Mathf.RoundToInt(dist / Mathf.Max(0.01f, _spacing)));
                // R2: bounded by n
                for (int k = 0; k < n; k++)
                {
                    Vector3 p = Vector3.Lerp(a, b, (float)k / n);
                    Quaternion rot = Quaternion.LookRotation(b - a);
                    ObjectPool.Instance.Get(_poolKey, p, rot);
                }
            }
        }
    }
}
