using UnityEngine;
using GameJamToolkit.Player;

namespace GameJamToolkit.Interaction
{
    /// <summary>Highlights the Renderer material when the player targets this object.</summary>
    public sealed class InteractableHighlight : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _highlightColor = new Color(1f, 0.85f, 0f, 1f);
        [SerializeField] private string _emissionParam = "_EmissionColor";

        private Color _baseEmission;
        private bool _hasBaseEmission;
        private PlayerInteraction _cachedInteraction;
        private float _nextCacheRefresh;

        private void Awake()
        {
            if (_renderer == null) _renderer = GetComponentInChildren<Renderer>();
            if (_renderer != null && _renderer.material.HasProperty(_emissionParam))
            {
                _baseEmission = _renderer.material.GetColor(_emissionParam);
                _hasBaseEmission = true;
            }
        }

        private void Update()
        {
            if (_renderer == null || !_hasBaseEmission) return;
            if (_cachedInteraction == null && Time.time >= _nextCacheRefresh)
            {
                _cachedInteraction = Object.FindAnyObjectByType<PlayerInteraction>();
                _nextCacheRefresh = Time.time + 1f; // R2: retry at most once per second
            }
            bool active = _cachedInteraction != null && _cachedInteraction.CurrentTarget == this.gameObject;
            _renderer.material.SetColor(_emissionParam, active ? _highlightColor : _baseEmission);
        }
    }
}
