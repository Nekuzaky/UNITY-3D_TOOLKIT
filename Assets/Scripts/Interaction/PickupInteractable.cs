using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;
using GameJamToolkit.Items;

namespace GameJamToolkit.Interaction
{
    /// <summary>Pickup variant that requires the interact key press.</summary>
    public sealed class PickupInteractable : InteractableBase
    {
        [SerializeField] private ItemBase _item;
        [Min(1)] [SerializeField] private int _count = 1;

        public override void Interact(GameObject source)
        {
            Debug.Assert(_item != null, "[PickupInteractable] _item is null"); // R5
            if (source == null || _item == null) return;
            var inv = source.GetComponentInParent<Inventory>();
            if (inv == null) return;
            if (!inv.TryAdd(_item, _count)) return;
            EventBus.Publish(new ItemPickedUpEvent { Picker = source, ItemId = _item.ItemId, Amount = _count });
            gameObject.SetActive(false);
        }
    }
}
