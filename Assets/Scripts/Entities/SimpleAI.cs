using UnityEngine;
using GameJamToolkit.Entities.States;

namespace GameJamToolkit.Entities
{
    /// <summary>
    /// Simple enemy AI: Idle -> Patrol (with waypoints) -> Chase when target is near
    /// -> Attack when in range. Reverses on cooldown.
    /// </summary>
    public sealed class SimpleAI : EnemyBase
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _stopDistance = 1.2f;

        private StateMachine _stateMachine;
        private IdleState _idle;
        private PatrolState _patrol;
        private ChaseState _chase;
        private AttackState _attack;

        protected override void Awake()
        {
            base.Awake();
            BuildStateMachine();
        }

        private void BuildStateMachine()
        {
            Debug.Assert(_config != null, "[SimpleAI.BuildStateMachine] _config is null"); // R5
            _stateMachine = new StateMachine();
            _idle = new IdleState();
            _patrol = new PatrolState(transform, _waypoints, _config != null ? _config.MoveSpeed * 0.6f : 1f);
            _chase = new ChaseState(transform, _target, _config != null ? _config.MoveSpeed : 3f, _stopDistance);
            _attack = new AttackState(transform, _target, _config != null ? _config.AttackRange : 1.5f, _config != null ? _config.Damage : 5f, _config != null ? _config.AttackCooldown : 1f);

            _stateMachine.Register(_idle);
            _stateMachine.Register(_patrol);
            _stateMachine.Register(_chase);
            _stateMachine.Register(_attack);
            _stateMachine.TransitionTo<IdleState>();
        }

        public override void SetTarget(Transform t)
        {
            base.SetTarget(t);
            BuildStateMachine();
        }

        private void Update()
        {
            if (!_health.IsAlive) return;
            DecideTransition();
            _stateMachine.Tick(Time.deltaTime);
        }

        private void DecideTransition()
        {
            if (_target == null)
            {
                if (_waypoints != null && _waypoints.Length > 0) _stateMachine.TransitionTo<PatrolState>();
                else _stateMachine.TransitionTo<IdleState>();
                return;
            }

            float d = Vector3.Distance(transform.position, _target.position);
            float sight = _config != null ? _config.SightRange : 8f;
            float range = _config != null ? _config.AttackRange : 1.5f;
            if (d <= range) _stateMachine.TransitionTo<AttackState>();
            else if (d <= sight) _stateMachine.TransitionTo<ChaseState>();
            else if (_waypoints != null && _waypoints.Length > 0) _stateMachine.TransitionTo<PatrolState>();
            else _stateMachine.TransitionTo<IdleState>();
        }
    }
}
