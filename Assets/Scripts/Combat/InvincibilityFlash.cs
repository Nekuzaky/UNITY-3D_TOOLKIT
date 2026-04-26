using System.Collections;
using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Blinks the mesh while the target is invincible after a hit.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class InvincibilityFlash : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private float _flashInterval = 0.08f;
        [SerializeField] private float _flashDuration = 0.4f;

        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            if (_renderers == null || _renderers.Length == 0) _renderers = GetComponentsInChildren<Renderer>();
            Debug.Assert(_health != null, "[InvincibilityFlash] _health is null"); // R5
        }

        private void OnEnable() { _health.OnDamageTaken += HandleDamage; }
        private void OnDisable() { _health.OnDamageTaken -= HandleDamage; }

        private void HandleDamage(DamageInfo info) { StartCoroutine(FlashRoutine()); }

        private IEnumerator FlashRoutine()
        {
            float t = 0f;
            // R2: bounded by _flashDuration
            while (t < _flashDuration)
            {
                SetEnabled(false);
                yield return new WaitForSeconds(_flashInterval);
                SetEnabled(true);
                yield return new WaitForSeconds(_flashInterval);
                t += _flashInterval * 2f;
            }
            SetEnabled(true);
        }

        private void SetEnabled(bool enabled)
        {
            if (_renderers == null) return;
            int max = _renderers.Length;
            for (int i = 0; i < max; i++)
            {
                if (_renderers[i] != null) _renderers[i].enabled = enabled;
            }
        }
    }
}
