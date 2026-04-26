using UnityEngine;

namespace GameJamToolkit.Vehicles
{
    /// <summary>Boat: forward thrust + slow yaw. Best paired with WaterFloat.</summary>
    public sealed class BoatController : VehicleControllerBase
    {
        [SerializeField] private float _waterDrag = 1.2f;

        private void Start()
        {
            _rigidbody.linearDamping = _waterDrag;
            _rigidbody.angularDamping = 2.5f;
        }

        private void FixedUpdate()
        {
            if (!IsControlEnabled || _rigidbody == null) return;
            Vector2 axis = _input.MoveAxis;
            if (CurrentSpeed < _maxSpeed) _rigidbody.AddForce(transform.forward * (_accelForce * axis.y), ForceMode.Force);
            transform.Rotate(0f, axis.x * _turnRate * Time.fixedDeltaTime, 0f, Space.Self);
        }
    }
}
