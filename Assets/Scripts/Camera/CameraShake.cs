using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Additive camera shake. Multiple Shake() calls accumulate.</summary>
    public sealed class CameraShake : MonoBehaviour
    {
        public static CameraShake ActiveInstance { get; private set; }

        [SerializeField] private float _decay = 4f;
        private Vector3 _currentOffset;
        private float _currentTrauma;

        private void Awake() { ActiveInstance = this; }
        private void OnDestroy() { if (ActiveInstance == this) ActiveInstance = null; }

        public void Shake(float trauma)
        {
            Debug.Assert(trauma >= 0f, "[CameraShake.Shake] trauma is negative"); // R5
            _currentTrauma = Mathf.Clamp01(_currentTrauma + trauma);
        }

        private void LateUpdate()
        {
            if (_currentTrauma <= 0f) { _currentOffset = Vector3.zero; return; }
            float intensity = _currentTrauma * _currentTrauma;
            _currentOffset = new Vector3(
                Random.Range(-1f, 1f) * intensity * 0.4f,
                Random.Range(-1f, 1f) * intensity * 0.4f,
                Random.Range(-1f, 1f) * intensity * 0.2f);
            transform.localPosition += _currentOffset;
            _currentTrauma = Mathf.Max(0f, _currentTrauma - _decay * Time.deltaTime);
        }
    }
}
