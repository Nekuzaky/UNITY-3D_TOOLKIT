using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.RTS
{
    /// <summary>
    /// Economy holding multiple resource types (wood, stone, food, ...).
    /// Resources are identified by string key.
    /// </summary>
    public sealed class ResourceEconomy : MonoBehaviour
    {
        [System.Serializable] public class Entry { public string Key; public int Amount; }
        [SerializeField] private Entry[] _initial;

        private readonly Dictionary<string, int> _stockDict = new Dictionary<string, int>();
        public event Action<string, int> OnChanged;

        private void Awake()
        {
            if (_initial == null) return;
            int max = _initial.Length; // R2
            for (int i = 0; i < max; i++)
            {
                if (_initial[i] == null || string.IsNullOrEmpty(_initial[i].Key)) continue;
                _stockDict[_initial[i].Key] = _initial[i].Amount;
            }
        }

        public int Get(string key) => _stockDict.TryGetValue(key, out var v) ? v : 0;

        public void Add(string key, int amount)
        {
            Debug.Assert(amount >= 0, "[ResourceEconomy.Add] amount is negative"); // R5
            if (string.IsNullOrEmpty(key) || amount <= 0) return;
            int prev = Get(key);
            _stockDict[key] = prev + amount;
            OnChanged?.Invoke(key, _stockDict[key]);
        }

        public bool TrySpend(string key, int amount)
        {
            Debug.Assert(amount >= 0, "[ResourceEconomy.TrySpend] amount is negative"); // R5
            if (Get(key) < amount) return false;
            _stockDict[key] -= amount;
            OnChanged?.Invoke(key, _stockDict[key]);
            return true;
        }
    }
}
