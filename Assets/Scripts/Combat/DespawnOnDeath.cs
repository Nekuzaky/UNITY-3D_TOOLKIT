using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Combat
{
    /// <summary>On death, returns to the pool or destroys. Optional delay for death animation.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class DespawnOnDeath : MonoBehaviour
    {
        [SerializeField] private string _poolKey;
        [Min(0f)] [SerializeField] private float _delay = 1.5f;

        private HealthComponent _health;
        private bool _scheduled;
        private float _despawnAt;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            Debug.Assert(_health != null, "[DespawnOnDeath] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDied += ScheduleDespawn; _scheduled = false; }
        private void OnDisable() { _health.OnDied -= ScheduleDespawn; }

        private void ScheduleDespawn()
        {
            if (_scheduled) return;
            _scheduled = true;
            _despawnAt = Time.time + _delay;
        }

        private void Update()
        {
            if (!_scheduled || Time.time < _despawnAt) return;
            _scheduled = false;
            if (!string.IsNullOrEmpty(_poolKey) && ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else gameObject.SetActive(false);
        }
    }
}
