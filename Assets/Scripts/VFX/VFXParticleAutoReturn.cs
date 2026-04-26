using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.VFX
{
    /// <summary>Attach to a particle prefab. When the system finishes, returns to the pool.</summary>
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class VFXParticleAutoReturn : MonoBehaviour
    {
        [SerializeField] private string _poolKey;

        private ParticleSystem _system;

        private void Awake()
        {
            _system = GetComponent<ParticleSystem>();
            Debug.Assert(_system != null, "[VFXParticleAutoReturn] _system null"); // R5
        }

        private void OnEnable()
        {
            if (_system != null) _system.Play();
        }

        private void Update()
        {
            if (_system == null) return;
            if (_system.IsAlive(true)) return;
            if (!string.IsNullOrEmpty(_poolKey) && ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else gameObject.SetActive(false);
        }
    }
}
