using UnityEngine;
using UnityEngine.AI;

namespace GameJamToolkit.Entities
{
    /// <summary>Chase as NavMeshAgent. Replan at a fixed interval to avoid spam.</summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshChase : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [Min(0.05f)] [SerializeField] private float _replanInterval = 0.25f;
        [SerializeField] private float _stopDistance = 1.2f;

        private NavMeshAgent _agent;
        private float _nextReplan;

        public Transform Target { get => _target; set => _target = value; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            Debug.Assert(_agent != null, "[NavMeshChase] _agent null"); // R5
            _agent.stoppingDistance = _stopDistance;
        }

        private void Update()
        {
            if (_target == null || _agent == null || !_agent.isOnNavMesh) return;
            if (Time.time < _nextReplan) return;
            _nextReplan = Time.time + _replanInterval;
            _agent.SetDestination(_target.position);
        }
    }
}
