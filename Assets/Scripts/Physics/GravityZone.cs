using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Changes the perceived gravity for Rigidbodies inside a zone (e.g. water, moon).</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class GravityZone : MonoBehaviour
    {
        [SerializeField] private Vector3 _zoneGravity = new Vector3(0f, -3f, 0f);

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerStay(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null || !rb.useGravity) return;
            // R7: cancel default gravity, then apply the zone gravity
            rb.AddForce(-Physics.gravity, ForceMode.Acceleration);
            rb.AddForce(_zoneGravity, ForceMode.Acceleration);
        }
    }
}
