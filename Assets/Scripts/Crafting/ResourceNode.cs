using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Items;

namespace GameJamToolkit.Crafting
{
    /// <summary>
    /// Harvestable resource (tree, ore, bush). Takes damage; on depletion drops
    /// items via Inventory.TryAdd or as a pickup.
    /// </summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class ResourceNode : MonoBehaviour
    {
        [SerializeField] private ItemBase _itemDropped;
        [SerializeField] private int _amountDropped = 1;
        [SerializeField] private string _pickupPoolKey = "ItemPickup";

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[ResourceNode] _health is null"); // R5
            Debug.Assert(_itemDropped != null, "[ResourceNode] _itemDropped is null"); // R5
        }

        private void OnEnable() { _health.OnDied += HandleDepleted; }
        private void OnDisable() { _health.OnDied -= HandleDepleted; }

        private void HandleDepleted()
        {
            if (!Core.ObjectPool.HasInstance) return;
            var go = Core.ObjectPool.Instance.Get(_pickupPoolKey, transform.position + Vector3.up, Quaternion.identity);
            if (go == null) return;
            var pickup = go.GetComponent<ItemPickup>();
            if (pickup != null) pickup.Configure(_itemDropped, _amountDropped);
            gameObject.SetActive(false);
        }
    }
}
