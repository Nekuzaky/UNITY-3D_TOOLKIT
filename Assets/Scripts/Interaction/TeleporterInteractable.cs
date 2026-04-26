using UnityEngine;

namespace GameJamToolkit.Interaction
{
    public sealed class TeleporterInteractable : InteractableBase
    {
        [SerializeField] private Transform _destination;

        public override void Interact(GameObject source)
        {
            Debug.Assert(_destination != null, "[TeleporterInteractable] _destination is null"); // R5
            if (source == null || _destination == null) return;
            var rb = source.GetComponent<Rigidbody>();
            if (rb != null) { rb.linearVelocity = Vector3.zero; rb.position = _destination.position; }
            else source.transform.position = _destination.position;
        }
    }
}
