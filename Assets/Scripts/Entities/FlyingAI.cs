using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>Flying AI: follows the target in 3D, hovers above it.</summary>
    public sealed class FlyingAI : EnemyBase
    {
        [SerializeField] private float _hoverHeight = 3f;
        [SerializeField] private float _bobAmplitude = 0.4f;
        [SerializeField] private float _bobFrequency = 1.5f;

        private float _phase;

        protected override void Awake()
        {
            base.Awake();
            _phase = Random.value * Mathf.PI * 2f;
        }

        private void Update()
        {
            if (!_health.IsAlive || _target == null) return;
            Vector3 desired = _target.position + Vector3.up * (_hoverHeight + Mathf.Sin((Time.time + _phase) * _bobFrequency) * _bobAmplitude);
            float speed = _config != null ? _config.MoveSpeed : 3f;
            transform.position = Vector3.MoveTowards(transform.position, desired, speed * Time.deltaTime);
            Vector3 lookDir = (_target.position - transform.position);
            if (lookDir.sqrMagnitude > 0.01f) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir.normalized), 6f * Time.deltaTime);
        }
    }
}
