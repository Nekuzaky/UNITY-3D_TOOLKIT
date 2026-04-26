using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Data
{
    /// <summary>Simple translation table key -> string.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Data/LocalizationTable", fileName = "LocalizationTable")]
    public sealed class LocalizationTable : ScriptableObject
    {
        [Serializable]
        public class Entry { public string Key; [TextArea] public string Value; }

        public string LanguageCode = "fr";
        [SerializeField] private Entry[] _entryArray;

        private Dictionary<string, string> _cache;

        private void OnEnable() { BuildCache(); }

        private void BuildCache()
        {
            _cache = new Dictionary<string, string>();
            if (_entryArray == null) return;
            int max = _entryArray.Length;
            for (int i = 0; i < max; i++)
            {
                if (_entryArray[i] == null || string.IsNullOrEmpty(_entryArray[i].Key)) continue;
                _cache[_entryArray[i].Key] = _entryArray[i].Value;
            }
        }

        public string Get(string key)
        {
            if (_cache == null) BuildCache();
            return _cache != null && _cache.TryGetValue(key, out var v) ? v : $"<{key}>";
        }
    }
}
