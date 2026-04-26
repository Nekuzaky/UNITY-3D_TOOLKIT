using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameJamToolkit.Items
{
    /// <summary>Inventory grid UI. Plugs into an Inventory and instantiates visual slots.</summary>
    public sealed class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private GameObject _slotPrefab;

        private InventorySlotUI[] _slotsArray;

        private void Start() { Build(); }

        private void OnEnable() { if (_inventory != null) _inventory.OnSlotChanged += RefreshSlot; }
        private void OnDisable() { if (_inventory != null) _inventory.OnSlotChanged -= RefreshSlot; }

        private void Build()
        {
            Debug.Assert(_inventory != null, "[InventoryUI.Build] _inventory is null"); // R5
            Debug.Assert(_slotPrefab != null, "[InventoryUI.Build] _slotPrefab is null"); // R5
            if (_inventory == null || _slotPrefab == null) return;

            _slotsArray = new InventorySlotUI[_inventory.Capacity];
            // R3 exempt: initial creation, not in gameplay
            int max = _inventory.Capacity;
            for (int i = 0; i < max; i++)
            {
                var go = Instantiate(_slotPrefab, _slotsRoot);
                var ui = go.GetComponent<InventorySlotUI>();
                _slotsArray[i] = ui;
                RefreshSlot(i);
            }
        }

        private void RefreshSlot(int index)
        {
            if (_slotsArray == null || index < 0 || index >= _slotsArray.Length) return;
            var slot = _inventory.GetSlot(index);
            _slotsArray[index].Refresh(slot);
        }
    }
}
