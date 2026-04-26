using System.Collections;
using UnityEngine;

namespace GameJamToolkit.VFX
{
    /// <summary>
    /// Generic hook to trigger a light pulse as MaterialProperty.
    /// (URP: plug onto a Renderer; for Volume PP, see URP docs).
    /// </summary>
    public sealed class PostProcessPulse : MonoBehaviour
    {
        [SerializeField] private Renderer _target;
        [SerializeField] private string _intensityProperty = "_PulseIntensity";
        [SerializeField] private float _peak = 1.5f;
        [SerializeField] private float _duration = 0.4f;

        public void Pulse()
        {
            Debug.Assert(_target != null, "[PostProcessPulse.Pulse] _target is null"); // R5
            if (_target == null) return;
            StopAllCoroutines();
            StartCoroutine(PulseRoutine());
        }

        private IEnumerator PulseRoutine()
        {
            float t = 0f;
            while (t < _duration)
            {
                t += Time.deltaTime;
                float k = 1f - (t / _duration);
                if (_target.material.HasProperty(_intensityProperty)) _target.material.SetFloat(_intensityProperty, _peak * k);
                yield return null;
            }
            if (_target.material.HasProperty(_intensityProperty)) _target.material.SetFloat(_intensityProperty, 0f);
        }
    }
}
