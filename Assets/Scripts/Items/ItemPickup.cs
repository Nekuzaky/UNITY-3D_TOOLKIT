using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Items
{
    /// <summary>Ground pickup. On Inventory entry, transfers and deactivates.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class ItemPickup : MonoBehaviour
    {
        [SerializeField] private ItemBase _item;
        [Min(1)] [SerializeField] private int _count = 1;
        [SerializeField] private string _poolKey;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Assert(_item != null, "[ItemPickup] _item is null"); // R5
            if (_item == null) return;

            var inv = other.GetComponentInParent<Inventory>();
            if (inv == null) return;
            if (!inv.TryAdd(_item, _count)) return;
            EventBus.Publish(new ItemPickedUpEvent { Picker = other.gameObject, ItemId = _item.ItemId, Amount = _count });

            if (!string.IsNullOrEmpty(_poolKey) && ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else gameObject.SetActive(false);
        }

        public void Configure(ItemBase item, int count) { _item = item; _count = Mathf.Max(1, count); }
    }
}
