using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Interaction
{
    /// <summary>Pressure plate: triggers when an object meeting the minimum mass is on top.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class PressurePlate : MonoBehaviour
    {
        [SerializeField] private float _minMass = 0.5f;
        [SerializeField] private UnityEvent _onPressed;
        [SerializeField] private UnityEvent _onReleased;

        private int _activeCount;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (!Qualifies(other)) return;
            _activeCount++;
            if (_activeCount == 1) _onPressed?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Qualifies(other)) return;
            _activeCount = Mathf.Max(0, _activeCount - 1);
            if (_activeCount == 0) _onReleased?.Invoke();
        }

        private bool Qualifies(Collider c)
        {
            var rb = c.attachedRigidbody;
            return rb != null && rb.mass >= _minMass;
        }
    }
}
