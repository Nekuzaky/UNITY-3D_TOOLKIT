using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Buoyancy: applies upward force when a Rigidbody crosses a water height.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class WaterFloat : MonoBehaviour
    {
        [SerializeField] private float _waterY = 0f;
        [SerializeField] private float _force = 30f;
        [SerializeField] private float _drag = 2f;
        [SerializeField] private LayerMask _affectedMask = ~0;

        private void OnTriggerStay(Collider other)
        {
            if (((1 << other.gameObject.layer) & _affectedMask) == 0) return;
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            float depth = _waterY - rb.position.y;
            if (depth <= 0f) return;
            rb.AddForce(Vector3.up * (_force * depth), ForceMode.Acceleration);
            rb.linearDamping = _drag;
        }

        private void OnTriggerExit(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb != null) rb.linearDamping = 0f;
        }

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }
    }
}
