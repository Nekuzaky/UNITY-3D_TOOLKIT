using UnityEngine;
using UnityEngine.AI;

namespace GameJamToolkit.AI
{
    /// <summary>Wanders within a radius around the spawn position. For simple mob enemies.</summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshWander : MonoBehaviour
    {
        [SerializeField] private float _radius = 6f;
        [SerializeField] private float _waitMin = 1f;
        [SerializeField] private float _waitMax = 3f;

        private NavMeshAgent _agent;
        private Vector3 _origin;
        private float _nextMoveAt;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _origin = transform.position;
            Debug.Assert(_agent != null, "[NavMeshWander] _agent null"); // R5
        }

        private void Update()
        {
            if (_agent == null || !_agent.isOnNavMesh) return;
            if (Time.time < _nextMoveAt) return;
            if (!_agent.pathPending && _agent.remainingDistance < 0.4f) PickNext();
        }

        private void PickNext()
        {
            Vector2 r = Random.insideUnitCircle * _radius;
            Vector3 desired = _origin + new Vector3(r.x, 0f, r.y);
            if (NavMesh.SamplePosition(desired, out var hit, _radius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
            _nextMoveAt = Time.time + Random.Range(_waitMin, _waitMax);
        }
    }
}
