using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Utils;

namespace GameJamToolkit.Entities
{
    /// <summary>Ranged enemy: keeps distance + fires with a RangedWeapon.</summary>
    public sealed class RangedAI : EnemyBase
    {
        [SerializeField] private RangedWeapon _weapon;
        [SerializeField] private float _preferredDistance = 6f;
        [SerializeField] private float _retreatDistance = 3f;

        private void Update()
        {
            if (!_health.IsAlive || _target == null) return;
            float d = Vector3.Distance(transform.position, _target.position);
            Vector3 dir = (_target.position - transform.position).normalized;

            if (d < _retreatDistance) transform.position -= dir * (_config != null ? _config.MoveSpeed : 3f) * Time.deltaTime;
            else if (d > _preferredDistance) transform.position += dir * (_config != null ? _config.MoveSpeed : 3f) * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 8f * Time.deltaTime);
            if (_weapon != null) _weapon.TryFire();
        }
    }
}
