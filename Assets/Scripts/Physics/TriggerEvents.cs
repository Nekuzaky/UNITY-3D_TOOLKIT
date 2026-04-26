using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Expose Enter/Exit/Stay en UnityEvents serialisables.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class TriggerEvents : MonoBehaviour
    {
        [System.Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

        [SerializeField] private string _filterTag;
        [SerializeField] private GameObjectEvent _onEnter;
        [SerializeField] private GameObjectEvent _onExit;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (!Match(other)) return;
            _onEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Match(other)) return;
            _onExit?.Invoke(other.gameObject);
        }

        private bool Match(Collider c)
        {
            return string.IsNullOrEmpty(_filterTag) || c.CompareTag(_filterTag);
        }
    }
}
