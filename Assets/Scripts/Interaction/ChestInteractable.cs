using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Items;

namespace GameJamToolkit.Interaction
{
    /// <summary>Chest: drops a LootTable contents on interaction.</summary>
    public sealed class ChestInteractable : InteractableBase
    {
        [SerializeField] private LootTable _table;
        [SerializeField] private string _pickupPoolKey = "ItemPickup";
        [SerializeField] private Animator _animator;
        [SerializeField] private string _openTrigger = "Open";

        public override void Interact(GameObject source)
        {
            if (_animator != null) _animator.SetTrigger(_openTrigger);
            if (_table == null || !ObjectPool.HasInstance) return;
            if (!_table.Roll(out var item, out var count)) return;
            var go = ObjectPool.Instance.Get(_pickupPoolKey, transform.position + Vector3.up, Quaternion.identity);
            var pickup = go != null ? go.GetComponent<ItemPickup>() : null;
            if (pickup != null) pickup.Configure(item, count);
        }
    }
}
