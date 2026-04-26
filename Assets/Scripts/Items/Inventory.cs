using System;
using UnityEngine;

namespace GameJamToolkit.Items
{
    /// <summary>
    /// Generic inventory with fixed slots. Configurable capacity.
    /// Notifies OnSlotChanged on every change to refresh the UI.
    /// </summary>
    public sealed class Inventory : MonoBehaviour
    {
        [Min(1)] [SerializeField] private int _capacity = 16;
        private InventorySlot[] _slots;
        public event Action<int> OnSlotChanged;

        public int Capacity => _capacity;
        public InventorySlot GetSlot(int index) => (index >= 0 && index < _slots.Length) ? _slots[index] : null;

        private void Awake()
        {
            Debug.Assert(_capacity > 0, "[Inventory.Awake] _capacity <= 0"); // R5
            _slots = new InventorySlot[_capacity];
            // R2: bounded by _capacity
            for (int i = 0; i < _capacity; i++) _slots[i] = new InventorySlot();
        }

        public bool TryAdd(ItemBase item, int count)
        {
            Debug.Assert(item != null, "[Inventory.TryAdd] item is null"); // R5
            Debug.Assert(count > 0, "[Inventory.TryAdd] count <= 0"); // R5
            if (item == null || count <= 0) return false;

            if (item.Stackable)
            {
                int max = _slots.Length; // R2
                for (int i = 0; i < max; i++)
                {
                    var s = _slots[i];
                    if (s.Item == item && s.Count < item.MaxStack)
                    {
                        int free = item.MaxStack - s.Count;
                        int put = Mathf.Min(free, count);
                        s.Count += put;
                        count -= put;
                        OnSlotChanged?.Invoke(i);
                        if (count == 0) return true;
                    }
                }
            }

            // Empty slots
            int slots = _slots.Length; // R2
            for (int i = 0; i < slots; i++)
            {
                if (_slots[i].IsEmpty)
                {
                    int put = item.Stackable ? Mathf.Min(item.MaxStack, count) : 1;
                    _slots[i].Item = item;
                    _slots[i].Count = put;
                    count -= put;
                    OnSlotChanged?.Invoke(i);
                    if (count == 0) return true;
                }
            }
            return false;
        }

        public bool TryRemove(int slotIndex, int count)
        {
            Debug.Assert(slotIndex >= 0 && slotIndex < _slots.Length, "[Inventory.TryRemove] index out of range"); // R5
            Debug.Assert(count > 0, "[Inventory.TryRemove] count <= 0"); // R5
            if (slotIndex < 0 || slotIndex >= _slots.Length) return false;
            var s = _slots[slotIndex];
            if (s.IsEmpty || s.Count < count) return false;
            s.Count -= count;
            if (s.Count <= 0) s.Clear();
            OnSlotChanged?.Invoke(slotIndex);
            return true;
        }

        public bool Has(ItemBase item, int count)
        {
            if (item == null) return false;
            int total = 0;
            int max = _slots.Length;
            for (int i = 0; i < max; i++) { if (_slots[i].Item == item) total += _slots[i].Count; }
            return total >= count;
        }
    }
}
