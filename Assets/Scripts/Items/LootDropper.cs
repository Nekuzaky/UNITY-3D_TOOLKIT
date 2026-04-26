using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Core;

namespace GameJamToolkit.Items
{
    /// <summary>On death, drops items per a LootTable. Uses the ObjectPool for pickups.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class LootDropper : MonoBehaviour
    {
        [SerializeField] private LootTable _table;
        [SerializeField] private string _pickupPoolKey = "ItemPickup";

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[LootDropper] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDied += HandleDeath; }
        private void OnDisable() { _health.OnDied -= HandleDeath; }

        private void HandleDeath()
        {
            if (_table == null) return;
            if (!_table.Roll(out var item, out var count)) return;
            if (item == null) return;
            if (!ObjectPool.HasInstance) return;

            var go = ObjectPool.Instance.Get(_pickupPoolKey, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            if (go == null) return;
            var pickup = go.GetComponent<ItemPickup>();
            if (pickup != null) pickup.Configure(item, count);
        }
    }
}
