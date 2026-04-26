using UnityEngine;
using UnityEngine.AI;

namespace GameJamToolkit.Entities
{
    /// <summary>Generic bridge between a NavMeshAgent and an Animator (Speed param).</summary>
    public sealed class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _speedParam = "Speed";

        private int _hash;

        private void Awake()
        {
            if (_agent == null) _agent = GetComponentInParent<NavMeshAgent>();
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            Debug.Assert(_animator != null, "[EnemyAnimator] _animator is null"); // R5
            _hash = Animator.StringToHash(_speedParam);
        }

        private void Update()
        {
            if (_animator == null) return;
            float speed = _agent != null ? _agent.velocity.magnitude : 0f;
            _animator.SetFloat(_hash, speed);
        }
    }
}
