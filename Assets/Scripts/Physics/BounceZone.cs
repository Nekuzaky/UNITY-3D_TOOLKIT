using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>3D trampoline: applies vertical velocity to any touching Rigidbody.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class BounceZone : MonoBehaviour
    {
        [SerializeField] private float _bounceForce = 12f;
        [SerializeField] private bool _resetVerticalBeforeAdd = true;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            Vector3 v = rb.linearVelocity;
            if (_resetVerticalBeforeAdd) v.y = 0f;
            v.y += _bounceForce;
            rb.linearVelocity = v;
        }
    }
}
