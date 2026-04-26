using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Items
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Items/ItemDatabase", fileName = "ItemDatabase")]
    public sealed class ItemDatabase : ScriptableObject
    {
        [SerializeField] private ItemBase[] _items;
        private Dictionary<string, ItemBase> _cache;

        private void OnEnable() { BuildCache(); }

        private void BuildCache()
        {
            _cache = new Dictionary<string, ItemBase>();
            if (_items == null) return;
            int max = _items.Length;
            for (int i = 0; i < max; i++)
            {
                if (_items[i] == null || string.IsNullOrEmpty(_items[i].ItemId)) continue;
                _cache[_items[i].ItemId] = _items[i];
            }
        }

        public ItemBase Get(string id) { if (_cache == null) BuildCache(); return _cache != null && _cache.TryGetValue(id, out var v) ? v : null; }
    }
}
