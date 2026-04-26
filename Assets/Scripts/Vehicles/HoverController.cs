using UnityEngine;

namespace GameJamToolkit.Vehicles
{
    /// <summary>
    /// Hovercraft: keeps a fixed ride height above ground via raycast spring,
    /// thrusts forward/back/strafe.
    /// </summary>
    public sealed class HoverController : VehicleControllerBase
    {
        [SerializeField] private float _rideHeight = 1.5f;
        [SerializeField] private float _springStrength = 80f;
        [SerializeField] private float _springDamping = 10f;
        [SerializeField] private LayerMask _groundMask = ~0;

        private void FixedUpdate()
        {
            if (!IsControlEnabled || _rigidbody == null) return;
            Hover();
            Vector2 axis = _input.MoveAxis;
            if (CurrentSpeed < _maxSpeed) _rigidbody.AddForce(transform.forward * (_accelForce * axis.y), ForceMode.Force);
            transform.Rotate(0f, axis.x * _turnRate * Time.fixedDeltaTime, 0f, Space.Self);
        }

        private void Hover()
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit, _rideHeight * 2f, _groundMask, QueryTriggerInteraction.Ignore)) return;
            float compression = _rideHeight - hit.distance;
            float vertVelocity = Vector3.Dot(_rigidbody.linearVelocity, Vector3.up);
            float force = (compression * _springStrength) - (vertVelocity * _springDamping);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Acceleration);
        }
    }
}
