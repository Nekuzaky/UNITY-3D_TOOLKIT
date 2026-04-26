using UnityEngine;

namespace GameJamToolkit.Sports
{
    /// <summary>Resets the ball to a spawn position; offers a Kick helper.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BallController : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private Rigidbody _rigidbody;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _initialPosition = _spawnPoint != null ? _spawnPoint.position : transform.position;
        }

        public void ResetToSpawn()
        {
            Debug.Assert(_rigidbody != null, "[BallController.ResetToSpawn] _rigidbody is null"); // R5
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.position = _initialPosition;
            transform.position = _initialPosition;
        }

        public void Kick(Vector3 direction, float force)
        {
            Debug.Assert(force >= 0f, "[BallController.Kick] force is negative"); // R5
            if (_rigidbody == null || force <= 0f) return;
            _rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}
