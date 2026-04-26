using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Puzzle
{
    /// <summary>Pressure switch that fires when total mass on top exceeds threshold.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class WeightSwitch : MonoBehaviour
    {
        [SerializeField] private float _activationMass = 1f;
        [SerializeField] private UnityEvent _onActivated;
        [SerializeField] private UnityEvent _onDeactivated;

        private float _currentMass;
        private bool _isActive;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            _currentMass += rb.mass;
            UpdateActive();
        }

        private void OnTriggerExit(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;
            _currentMass = Mathf.Max(0f, _currentMass - rb.mass);
            UpdateActive();
        }

        private void UpdateActive()
        {
            bool active = _currentMass >= _activationMass;
            if (active == _isActive) return;
            _isActive = active;
            if (_isActive) _onActivated?.Invoke();
            else _onDeactivated?.Invoke();
        }
    }
}
