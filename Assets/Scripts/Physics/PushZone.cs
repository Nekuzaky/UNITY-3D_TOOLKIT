using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Wind / propulsion: pushes Rigidbodies in a direction while inside the trigger.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class PushZone : MonoBehaviour
    {
        [SerializeField] private Vector3 _force = new Vector3(0f, 0f, 10f);
        [SerializeField] private bool _localSpace = true;
        [SerializeField] private ForceMode _mode = ForceMode.Acceleration;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerStay(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            Vector3 dir = _localSpace ? transform.TransformDirection(_force) : _force;
            rb.AddForce(dir, _mode);
        }
    }
}
