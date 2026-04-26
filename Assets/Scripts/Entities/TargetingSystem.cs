using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>Finds and exposes the closest target by tag + range.</summary>
    public sealed class TargetingSystem : MonoBehaviour
    {
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private float _searchRadius = 12f;
        [Min(0.05f)] [SerializeField] private float _refreshInterval = 0.4f;

        private float _nextRefresh;
        public Transform Target { get; private set; }

        private void Update()
        {
            if (Time.time < _nextRefresh) return;
            _nextRefresh = Time.time + _refreshInterval;
            Target = FindClosest();
        }

        private Transform FindClosest()
        {
            var list = GameObject.FindGameObjectsWithTag(_targetTag);
            if (list == null || list.Length == 0) return null;
            int max = list.Length; // R2
            float bestSqr = _searchRadius * _searchRadius;
            Transform best = null;
            for (int i = 0; i < max; i++)
            {
                if (list[i] == null) continue;
                float d = (list[i].transform.position - transform.position).sqrMagnitude;
                if (d <= bestSqr) { bestSqr = d; best = list[i].transform; }
            }
            return best;
        }
    }
}
