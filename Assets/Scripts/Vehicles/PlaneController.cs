using UnityEngine;

namespace GameJamToolkit.Vehicles
{
    /// <summary>
    /// Arcade plane: throttle + pitch + yaw + roll. Lift scales with forward
    /// speed. Plug a separate gravity (off useGravity) and let lift carry it.
    /// </summary>
    public sealed class PlaneController : VehicleControllerBase
    {
        [SerializeField] private float _liftCoefficient = 14f;
        [SerializeField] private float _pitchRate = 60f;
        [SerializeField] private float _rollRate = 90f;
        [SerializeField] private float _yawRate = 30f;

        private float _throttle;

        private void Start()
        {
            _rigidbody.useGravity = true;
            _rigidbody.linearDamping = 0.05f;
            _rigidbody.angularDamping = 1.5f;
        }

        private void FixedUpdate()
        {
            if (!IsControlEnabled || _rigidbody == null) return;
            Vector2 axis = _input.MoveAxis;

            _throttle = Mathf.Clamp01(_throttle + axis.y * Time.fixedDeltaTime);
            _rigidbody.AddForce(transform.forward * (_accelForce * _throttle), ForceMode.Force);

            float pitch = -axis.y * _pitchRate * Time.fixedDeltaTime;
            float roll = axis.x * _rollRate * Time.fixedDeltaTime;
            // banked turn: yaw proportional to current roll angle (simulates lift redirection)
            float currentRoll = Mathf.DeltaAngle(0f, transform.eulerAngles.z);
            float yaw = -currentRoll / 90f * _yawRate * Time.fixedDeltaTime;
            transform.Rotate(pitch, yaw, -roll, Space.Self);

            float forwardSpeed = Vector3.Dot(_rigidbody.linearVelocity, transform.forward);
            float lift = Mathf.Max(0f, forwardSpeed) * _liftCoefficient;
            _rigidbody.AddForce(transform.up * lift, ForceMode.Force);
        }

        public void SetThrottle(float value) { _throttle = Mathf.Clamp01(value); }
    }
}
