using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Fires a UnityEvent after a FixedTime delay. Useful for physics-driven scripts.</summary>
    public sealed class PhysicsTimer : MonoBehaviour
    {
        [SerializeField] private float _seconds = 1f;
        [SerializeField] private bool _autoStart = true;
        [SerializeField] private UnityEvent _onElapsed;

        private float _runAt;
        private bool _scheduled;

        private void Start() { if (_autoStart) Schedule(_seconds); }

        public void Schedule(float seconds)
        {
            _runAt = Time.fixedTime + Mathf.Max(0f, seconds);
            _scheduled = true;
        }

        private void FixedUpdate()
        {
            if (!_scheduled || Time.fixedTime < _runAt) return;
            _scheduled = false;
            _onElapsed?.Invoke();
        }
    }
}
