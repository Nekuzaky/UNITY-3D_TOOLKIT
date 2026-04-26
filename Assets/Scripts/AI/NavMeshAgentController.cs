using UnityEngine;
using UnityEngine.AI;

namespace GameJamToolkit.AI
{
    /// <summary>Helper wrappers around a NavMeshAgent (set destination, stop, reach...).</summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshAgentController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;

        private void Awake() { _agent = GetComponent<NavMeshAgent>(); Debug.Assert(_agent != null, "[NavMeshAgentController] _agent null"); }

        public void GoTo(Vector3 worldPos)
        {
            if (_agent == null || !_agent.isOnNavMesh) return;
            _agent.SetDestination(worldPos);
        }

        public bool HasReached(float threshold = 0.2f)
        {
            return _agent != null && !_agent.pathPending && _agent.remainingDistance <= threshold;
        }

        public void Stop() { if (_agent != null) _agent.ResetPath(); }
    }
}
