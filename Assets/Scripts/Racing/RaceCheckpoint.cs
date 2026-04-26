using UnityEngine;

namespace GameJamToolkit.Racing
{
    /// <summary>
    /// Sequenced race checkpoint. Triggers progress only when crossed by a
    /// participant; the manager validates ordering to prevent shortcuts.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class RaceCheckpoint : MonoBehaviour
    {
        [SerializeField] private int _index;
        public int Index => _index;

        public event System.Action<int, GameObject> OnReached;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            var go = other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : other.gameObject;
            OnReached?.Invoke(_index, go);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, new Vector3(8f, 4f, 0.5f));
        }
    }
}
