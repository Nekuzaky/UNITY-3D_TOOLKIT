using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Trigger that fires only once in the scene's lifetime.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerOnce : MonoBehaviour
    {
        [SerializeField] private string _filterTag = "Player";
        [SerializeField] private UnityEvent _onTrigger;

        private bool _fired;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (_fired) return;
            if (!string.IsNullOrEmpty(_filterTag) && !other.CompareTag(_filterTag)) return;
            _fired = true;
            _onTrigger?.Invoke();
        }
    }
}
