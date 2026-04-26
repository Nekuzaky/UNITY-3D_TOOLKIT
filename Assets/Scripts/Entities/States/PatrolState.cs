using UnityEngine;

namespace GameJamToolkit.Entities.States
{
    /// <summary>Patrol between waypoints. Reusable outside AI.</summary>
    public sealed class PatrolState : StateBase
    {
        private readonly Transform _self;
        private readonly Transform[] _waypoints;
        private readonly float _speed;
        private int _currentIndex;

        public PatrolState(Transform self, Transform[] waypoints, float speed)
        {
            _self = self;
            _waypoints = waypoints;
            _speed = Mathf.Max(0f, speed);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_self == null || _waypoints == null || _waypoints.Length == 0) return;
            var target = _waypoints[_currentIndex];
            if (target == null) return;
            Vector3 dir = (target.position - _self.position);
            if (dir.sqrMagnitude < 0.05f)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
                return;
            }
            _self.position += dir.normalized * _speed * deltaTime;
        }
    }
}
