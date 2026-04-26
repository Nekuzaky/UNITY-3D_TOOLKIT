using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Ground detection as downward raycast. Plug into controllers.</summary>
    public sealed class GroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask = ~0;
        [SerializeField] private float _origin = 0.1f;
        [SerializeField] private float _distance = 0.2f;

        public bool IsGrounded { get; private set; }
        public Vector3 GroundNormal { get; private set; } = Vector3.up;

        private void Update()
        {
            Vector3 from = transform.position + Vector3.up * _origin;
            if (Physics.Raycast(from, Vector3.down, out var hit, _origin + _distance, _groundMask, QueryTriggerInteraction.Ignore))
            {
                IsGrounded = true;
                GroundNormal = hit.normal;
            }
            else
            {
                IsGrounded = false;
                GroundNormal = Vector3.up;
            }
        }
    }
}
