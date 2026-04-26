using UnityEngine;

namespace GameJamToolkit.Puzzle
{
    /// <summary>
    /// Object that can be picked up. The grabber freezes its physics and parents
    /// it to the carry anchor; release restores the original physics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class Grabbable : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private bool _wasKinematic;
        private bool _wasGravity;
        public bool IsGrabbed { get; private set; }
        public Transform CurrentHolder { get; private set; }

        private void Awake() { _rigidbody = GetComponent<Rigidbody>(); }

        public void Grab(Transform holder)
        {
            Debug.Assert(holder != null, "[Grabbable.Grab] holder is null"); // R5
            if (IsGrabbed || _rigidbody == null) return;
            _wasKinematic = _rigidbody.isKinematic;
            _wasGravity = _rigidbody.useGravity;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            IsGrabbed = true;
            CurrentHolder = holder;
        }

        public void Release(Vector3 throwVelocity)
        {
            if (!IsGrabbed || _rigidbody == null) return;
            transform.SetParent(null);
            _rigidbody.isKinematic = _wasKinematic;
            _rigidbody.useGravity = _wasGravity;
            _rigidbody.linearVelocity = throwVelocity;
            IsGrabbed = false;
            CurrentHolder = null;
        }
    }
}
