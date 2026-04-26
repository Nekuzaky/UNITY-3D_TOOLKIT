using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Utils;

namespace GameJamToolkit.Entities.States
{
    /// <summary>Fixed-range attack + cooldown. Damage source: DamageInfo built here.</summary>
    public sealed class AttackState : StateBase
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly float _range;
        private readonly float _damage;
        private Cooldown _cooldown;

        public AttackState(Transform self, Transform target, float range, float damage, float cooldown)
        {
            _self = self;
            _target = target;
            _range = Mathf.Max(0f, range);
            _damage = Mathf.Max(0f, damage);
            _cooldown = Cooldown.Of(Mathf.Max(0.05f, cooldown));
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_self == null || _target == null) return;
            float dist = Vector3.Distance(_self.position, _target.position);
            if (dist > _range) return;
            if (!_cooldown.TryConsume()) return;

            DamageHandler.TryDamage(_target.gameObject, new DamageInfo
            {
                Amount = _damage,
                Type = DamageType.Physical,
                Source = _self.gameObject,
                HitPoint = _target.position,
                HitNormal = (_self.position - _target.position).normalized,
                Knockback = 2f
            });
        }
    }
}
