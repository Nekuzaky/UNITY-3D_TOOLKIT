using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Bridges a HealthComponent and a Knockback: on every hit applies
    /// knockback in the opposite direction from the source.
    /// </summary>
    [RequireComponent(typeof(HealthComponent), typeof(Knockback))]
    public sealed class KnockbackHook : MonoBehaviour
    {
        private HealthComponent _health;
        private Knockback _knockback;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _knockback = GetComponent<Knockback>();
            Debug.Assert(_health != null, "[KnockbackHook] _health is null"); // R5
            Debug.Assert(_knockback != null, "[KnockbackHook] _knockback null"); // R5
        }

        private void OnEnable() { _health.OnDamageTaken += HandleDamage; }
        private void OnDisable() { _health.OnDamageTaken -= HandleDamage; }

        private void HandleDamage(DamageInfo info)
        {
            Vector3 origin = info.Source != null ? info.Source.transform.position : info.HitPoint;
            _knockback.Apply(origin, info.Knockback);
        }
    }
}
