using UnityEngine;

namespace GameJamToolkit.Vehicles
{
    /// <summary>
    /// Arcade car. Forward/back via throttle, yaw via steering. Not a realistic
    /// wheel-collider sim; suitable for jam-grade driving.
    /// </summary>
    public sealed class CarController : VehicleControllerBase
    {
        [SerializeField] private float _brakeForce = 2500f;
        [SerializeField] private float _drag = 0.3f;
        [SerializeField] private float _angularDrag = 2f;
        [SerializeField] private float _gravityBoost = 9.81f;

        private void Start()
        {
            _rigidbody.linearDamping = _drag;
            _rigidbody.angularDamping = _angularDrag;
            _rigidbody.centerOfMass = new Vector3(0f, -0.4f, 0f);
        }

        private void FixedUpdate()
        {
            if (!IsControlEnabled || _rigidbody == null) return;
            Vector2 axis = _input.MoveAxis;
            ApplyThrottle(axis.y);
            ApplySteering(axis.x);
            _rigidbody.AddForce(Vector3.down * _gravityBoost, ForceMode.Acceleration);
        }

        private void ApplyThrottle(float input)
        {
            if (CurrentSpeed >= _maxSpeed && input > 0f) return;
            if (input < 0f && Vector3.Dot(transform.forward, _rigidbody.linearVelocity) > 0.1f)
            {
                _rigidbody.AddForce(-transform.forward * _brakeForce, ForceMode.Force);
                return;
            }
            _rigidbody.AddForce(transform.forward * (_accelForce * input), ForceMode.Force);
        }

        private void ApplySteering(float input)
        {
            float speedFactor = Mathf.Clamp01(CurrentSpeed / _maxSpeed);
            float yaw = input * _turnRate * speedFactor * Time.fixedDeltaTime;
            transform.Rotate(0f, yaw, 0f, Space.Self);
        }
    }
}
