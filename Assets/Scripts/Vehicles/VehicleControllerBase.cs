using UnityEngine;
using GameJamToolkit.Player;

namespace GameJamToolkit.Vehicles
{
    /// <summary>
    /// Abstract base for any vehicle (car, boat, plane, hovercraft). Holds the
    /// shared input read and Rigidbody reference. Subclass to implement the
    /// physics specific to the vehicle type.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class VehicleControllerBase : MonoBehaviour
    {
        [SerializeField] protected InputHandler _input;
        [SerializeField] protected float _maxSpeed = 30f;
        [SerializeField] protected float _accelForce = 1500f;
        [SerializeField] protected float _turnRate = 60f;

        protected Rigidbody _rigidbody;
        public bool IsControlEnabled { get; protected set; } = true;
        public float CurrentSpeed => _rigidbody != null ? _rigidbody.linearVelocity.magnitude : 0f;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_input == null) _input = GetComponent<InputHandler>();
            Debug.Assert(_rigidbody != null, "[VehicleControllerBase] _rigidbody is null"); // R5
            Debug.Assert(_input != null, "[VehicleControllerBase] _input is null"); // R5
        }

        public void EnableControl() { IsControlEnabled = true; }
        public void DisableControl() { IsControlEnabled = false; }
    }
}
