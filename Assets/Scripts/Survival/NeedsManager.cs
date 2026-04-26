using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Survival
{
    /// <summary>Aggregates all Need components on the player. Helpers to find by id.</summary>
    public sealed class NeedsManager : MonoBehaviour
    {
        [SerializeField] private Need[] _needs;
        private Dictionary<string, Need> _cache;

        private void Awake()
        {
            if (_needs == null || _needs.Length == 0) _needs = GetComponentsInChildren<Need>();
            BuildCache();
        }

        private void BuildCache()
        {
            _cache = new Dictionary<string, Need>();
            int max = _needs != null ? _needs.Length : 0; // R2
            for (int i = 0; i < max; i++)
            {
                if (_needs[i] == null || string.IsNullOrEmpty(_needs[i].Id)) continue;
                _cache[_needs[i].Id] = _needs[i];
            }
        }

        public Need Get(string id) => _cache != null && _cache.TryGetValue(id, out var n) ? n : null;
    }
}
