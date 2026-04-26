using UnityEngine;

namespace GameJamToolkit.VFX
{
    /// <summary>Toggles a TrailRenderer based on a speed threshold. Clears its trail on reset.</summary>
    [RequireComponent(typeof(TrailRenderer))]
    public sealed class TrailEffect : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _minSpeed = 4f;

        private TrailRenderer _trail;

        private void Awake() { _trail = GetComponent<TrailRenderer>(); Debug.Assert(_trail != null, "[TrailEffect] _trail null"); }

        private void Update()
        {
            if (_rigidbody == null || _trail == null) return;
            bool active = _rigidbody.linearVelocity.magnitude >= _minSpeed;
            _trail.emitting = active;
        }
    }
}
