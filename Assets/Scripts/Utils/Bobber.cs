using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Sine vertical motion. Floating visual.</summary>
    public sealed class Bobber : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 0.2f;
        [SerializeField] private float _frequency = 1.5f;

        private Vector3 _origin;

        private void Awake() { _origin = transform.localPosition; }

        private void Update()
        {
            float y = Mathf.Sin(Time.time * _frequency * Mathf.PI * 2f) * _amplitude;
            transform.localPosition = _origin + new Vector3(0f, y, 0f);
        }
    }
}
