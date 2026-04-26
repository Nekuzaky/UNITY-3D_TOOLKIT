using UnityEngine;

namespace GameJamToolkit.Entities.States
{
    public sealed class ChaseState : StateBase
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly float _speed;
        private readonly float _stopDistance;

        public ChaseState(Transform self, Transform target, float speed, float stopDistance)
        {
            _self = self;
            _target = target;
            _speed = Mathf.Max(0f, speed);
            _stopDistance = Mathf.Max(0f, stopDistance);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_self == null || _target == null) return;
            Vector3 to = _target.position - _self.position;
            if (to.sqrMagnitude <= _stopDistance * _stopDistance) return;
            _self.position += to.normalized * _speed * deltaTime;
            _self.rotation = Quaternion.Slerp(_self.rotation, Quaternion.LookRotation(to.normalized), 8f * deltaTime);
        }
    }
}
