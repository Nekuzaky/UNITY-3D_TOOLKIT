using UnityEngine;

namespace GameJamToolkit.Interaction
{
    /// <summary>Basic door: pivots around Y on activation.</summary>
    public sealed class DoorInteractable : InteractableBase
    {
        [SerializeField] private Transform _hinge;
        [SerializeField] private float _openAngle = 90f;
        [SerializeField] private float _speed = 4f;
        [SerializeField] private bool _startOpen = false;
        [SerializeField] private string _requiredKey;

        private bool _isOpen;
        private Quaternion _closedRotation;
        private Quaternion _openRotation;

        private void Awake()
        {
            Debug.Assert(_hinge != null, "[DoorInteractable] _hinge null"); // R5
            _closedRotation = _hinge.localRotation;
            _openRotation = _closedRotation * Quaternion.Euler(0f, _openAngle, 0f);
            _isOpen = _startOpen;
        }

        private void Update()
        {
            if (_hinge == null) return;
            Quaternion target = _isOpen ? _openRotation : _closedRotation;
            _hinge.localRotation = Quaternion.Slerp(_hinge.localRotation, target, _speed * Time.deltaTime);
        }

        public override bool CanInteract(GameObject source)
        {
            if (!base.CanInteract(source)) return false;
            if (string.IsNullOrEmpty(_requiredKey)) return true;
            // R7: do not trust, verify the inventory
            var inv = source != null ? source.GetComponentInParent<Items.Inventory>() : null;
            // Note: simplified check. Iterate slots looking for a matching key id.
            return inv != null && HasKey(inv, _requiredKey);
        }

        private static bool HasKey(Items.Inventory inv, string keyId)
        {
            int max = inv.Capacity;
            for (int i = 0; i < max; i++)
            {
                var slot = inv.GetSlot(i);
                if (slot == null || slot.IsEmpty) continue;
                if (slot.Item is Items.KeyItem k && k.LockId == keyId) return true;
            }
            return false;
        }

        public override void Interact(GameObject source) { _isOpen = !_isOpen; }
    }
}
