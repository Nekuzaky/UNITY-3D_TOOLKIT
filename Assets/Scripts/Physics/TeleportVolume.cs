using UnityEngine;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Teleports any incoming object to a target.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class TeleportVolume : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        [SerializeField] private bool _zeroVelocity = true;
        [SerializeField] private string _filterTag = "Player";

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Assert(_destination != null, "[TeleportVolume] _destination is null"); // R5
            if (_destination == null) return;
            if (!string.IsNullOrEmpty(_filterTag) && !other.CompareTag(_filterTag)) return;
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                if (_zeroVelocity) rb.linearVelocity = Vector3.zero;
                rb.position = _destination.position;
                rb.MoveRotation(_destination.rotation);
            }
            else
            {
                other.transform.SetPositionAndRotation(_destination.position, _destination.rotation);
            }
        }
    }
}
