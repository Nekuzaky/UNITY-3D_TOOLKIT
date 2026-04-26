using System.Collections;
using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.VFX
{
    /// <summary>Brief white flash on a Renderer when its HealthComponent takes damage.</summary>
    [RequireComponent(typeof(HealthComponent))]
    public sealed class HitFlash : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashSeconds = 0.06f;
        [SerializeField] private string _baseColorParam = "_BaseColor";

        private HealthComponent _health;
        private Color _origColor;
        private bool _hasOrig;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            if (_renderer == null) _renderer = GetComponentInChildren<Renderer>();
            if (_renderer != null && _renderer.material.HasProperty(_baseColorParam))
            {
                _origColor = _renderer.material.GetColor(_baseColorParam);
                _hasOrig = true;
            }
            Debug.Assert(_health != null, "[HitFlash] _health is null"); // R5
        }

        private void OnEnable() { if (_health != null) _health.OnDamageTaken += HandleDamage; }
        private void OnDisable() { if (_health != null) _health.OnDamageTaken -= HandleDamage; }

        private void HandleDamage(DamageInfo info) { StopAllCoroutines(); StartCoroutine(FlashRoutine()); }

        private IEnumerator FlashRoutine()
        {
            if (_renderer == null || !_hasOrig) yield break;
            _renderer.material.SetColor(_baseColorParam, _flashColor);
            yield return new WaitForSeconds(_flashSeconds);
            _renderer.material.SetColor(_baseColorParam, _origColor);
        }
    }
}
