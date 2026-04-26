using System;
using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Data
{
    /// <summary>Saves the current HP of a HealthComponent.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class SaveableHealth : MonoBehaviour, ISaveable
    {
        [SerializeField] private string _saveKey = "health";
        private HealthComponent _health;
        public string SaveKey => _saveKey;

        [Serializable] private class State { public float Current; public float Max; }

        private void Awake() { _health = GetComponent<HealthComponent>(); Debug.Assert(_health != null, "[SaveableHealth] _health is null"); }
        private void OnEnable() { if (SaveManager.HasInstance) SaveManager.Instance.Register(this); }
        private void OnDisable() { if (SaveManager.HasInstance) SaveManager.Instance.Unregister(this); }

        public string SerializeState()
        {
            return JsonUtility.ToJson(new State { Current = _health.Current, Max = _health.Max });
        }

        public void DeserializeState(string state)
        {
            if (string.IsNullOrEmpty(state) || _health == null) return;
            var s = JsonUtility.FromJson<State>(state);
            if (s == null) return;
            _health.SetMaxHealth(s.Max, false);
            // R7: cannot set Current directly, heal / damage works around it
            float diff = s.Current - _health.Current;
            if (diff > 0f) _health.Heal(diff);
            else if (diff < 0f) _health.TakeDamage(DamageInfo.Default(-diff, gameObject));
        }
    }
}
