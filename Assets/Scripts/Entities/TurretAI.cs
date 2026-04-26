using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>Static turret: tracking + firing on the closest target.</summary>
    public sealed class TurretAI : EnemyBase
    {
        [SerializeField] private RangedWeapon _weapon;
        [SerializeField] private Transform _yawJoint;
        [SerializeField] private Transform _pitchJoint;
        [SerializeField] private float _aimSpeed = 4f;

        private void Update()
        {
            if (!_health.IsAlive || _target == null) return;
            AimAt(_target.position);
            if (_weapon != null && IsAimedAt(_target.position, 0.97f)) _weapon.TryFire();
        }

        private void AimAt(Vector3 worldTarget)
        {
            if (_yawJoint != null)
            {
                Vector3 lateral = worldTarget - _yawJoint.position;
                lateral.y = 0f;
                if (lateral.sqrMagnitude > 0.001f)
                {
                    Quaternion goal = Quaternion.LookRotation(lateral.normalized);
                    _yawJoint.rotation = Quaternion.Slerp(_yawJoint.rotation, goal, _aimSpeed * Time.deltaTime);
                }
            }
            if (_pitchJoint != null)
            {
                Vector3 to = worldTarget - _pitchJoint.position;
                if (to.sqrMagnitude > 0.001f)
                {
                    Quaternion goal = Quaternion.LookRotation(to.normalized);
                    _pitchJoint.rotation = Quaternion.Slerp(_pitchJoint.rotation, goal, _aimSpeed * Time.deltaTime);
                }
            }
        }

        private bool IsAimedAt(Vector3 worldTarget, float threshold)
        {
            Vector3 fwd = (_pitchJoint != null ? _pitchJoint.forward : transform.forward);
            Vector3 dir = (worldTarget - transform.position).normalized;
            return Vector3.Dot(fwd, dir) >= threshold;
        }
    }
}
