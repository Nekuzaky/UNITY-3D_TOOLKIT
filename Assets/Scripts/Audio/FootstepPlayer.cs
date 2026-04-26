using UnityEngine;
using GameJamToolkit.Player;

namespace GameJamToolkit.Audio
{
    /// <summary>Plays a footstep at an interval inversely proportional to movement speed.</summary>
    public sealed class FootstepPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerControllerBase _controller;
        [SerializeField] private string _sfxId = "footstep";
        [Min(0.05f)] [SerializeField] private float _baseInterval = 0.45f;
        [Min(0.01f)] [SerializeField] private float _minSpeed = 0.5f;

        private float _nextStepAt;

        private void Awake()
        {
            if (_controller == null) _controller = GetComponentInParent<PlayerControllerBase>();
            Debug.Assert(_controller != null, "[FootstepPlayer] _controller is null"); // R5
        }

        private void Update()
        {
            if (_controller == null) return;
            Vector3 v = _controller.CurrentVelocity;
            v.y = 0f;
            float speed = v.magnitude;
            if (speed < _minSpeed) return;
            if (Time.time < _nextStepAt) return;
            float interval = _baseInterval / Mathf.Max(0.1f, speed / 5f);
            _nextStepAt = Time.time + interval;
            if (AudioManager.HasInstance) AudioManager.Instance.PlaySfxAt(_sfxId, transform.position);
        }
    }
}
