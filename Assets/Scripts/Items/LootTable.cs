using System;
using UnityEngine;

namespace GameJamToolkit.Items
{
    /// <summary>Weighted loot table. For enemy drops, chests, etc.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Items/LootTable", fileName = "LootTable")]
    public sealed class LootTable : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public ItemBase Item;
            [Min(0f)] public float Weight = 1f;
            [Min(1)] public int MinCount = 1;
            [Min(1)] public int MaxCount = 1;
        }

        [SerializeField] private Entry[] _entries;
        [Range(0f, 1f)] [SerializeField] private float _dropChance = 0.7f;

        public bool Roll(out ItemBase item, out int count)
        {
            item = null;
            count = 0;
            if (_entries == null || _entries.Length == 0) return false;
            if (UnityEngine.Random.value > _dropChance) return false;

            float total = 0f;
            int max = _entries.Length;
            for (int i = 0; i < max; i++) { if (_entries[i] != null) total += Mathf.Max(0f, _entries[i].Weight); }
            if (total <= 0f) return false;

            float r = UnityEngine.Random.Range(0f, total);
            float acc = 0f;
            for (int i = 0; i < max; i++)
            {
                if (_entries[i] == null) continue;
                acc += Mathf.Max(0f, _entries[i].Weight);
                if (r > acc) continue;
                item = _entries[i].Item;
                count = UnityEngine.Random.Range(_entries[i].MinCount, _entries[i].MaxCount + 1);
                return item != null;
            }
            return false;
        }
    }
}
