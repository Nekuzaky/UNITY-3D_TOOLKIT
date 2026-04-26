using UnityEngine;

namespace GameJamToolkit.Entities.States
{
    public sealed class FleeState : StateBase
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly float _speed;
        private readonly float _safeDistance;

        public FleeState(Transform self, Transform target, float speed, float safeDistance)
        {
            _self = self;
            _target = target;
            _speed = Mathf.Max(0f, speed);
            _safeDistance = Mathf.Max(0f, safeDistance);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_self == null || _target == null) return;
            Vector3 away = _self.position - _target.position;
            if (away.sqrMagnitude > _safeDistance * _safeDistance) return;
            _self.position += away.normalized * _speed * deltaTime;
        }
    }
}
