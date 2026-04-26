using UnityEngine;

namespace GameJamToolkit.AI
{
    /// <summary>Vision cone: detects targets within angle + distance + clear line of sight.</summary>
    public sealed class FieldOfView : MonoBehaviour
    {
        [SerializeField] private float _viewRadius = 8f;
        [SerializeField] [Range(1f, 360f)] private float _viewAngle = 90f;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] [Min(0.1f)] private float _refreshInterval = 0.2f;

        private Transform _visibleTarget;
        public Transform VisibleTarget => _visibleTarget;
        private float _nextRefresh;

        private void Update()
        {
            if (Time.time < _nextRefresh) return;
            _nextRefresh = Time.time + _refreshInterval;
            FindVisibleTarget();
        }

        private void FindVisibleTarget()
        {
            _visibleTarget = null;
            var hits = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
            int max = hits.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var t = hits[i].transform;
                Vector3 dir = (t.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dir) > _viewAngle * 0.5f) continue;
                float dist = Vector3.Distance(transform.position, t.position);
                if (Physics.Raycast(transform.position, dir, dist, _obstacleMask, QueryTriggerInteraction.Ignore)) continue;
                _visibleTarget = t;
                return;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);
            Vector3 left = Quaternion.AngleAxis(-_viewAngle * 0.5f, Vector3.up) * transform.forward;
            Vector3 right = Quaternion.AngleAxis(_viewAngle * 0.5f, Vector3.up) * transform.forward;
            Gizmos.DrawRay(transform.position, left * _viewRadius);
            Gizmos.DrawRay(transform.position, right * _viewRadius);
        }
    }
}
