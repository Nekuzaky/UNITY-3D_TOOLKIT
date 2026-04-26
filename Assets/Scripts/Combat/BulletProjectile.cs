using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Straight projectile. Raycast detection to avoid tunneling.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class BulletProjectile : ProjectileBase
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        public override void Launch(ProjectileLaunchInfo info)
        {
            base.Launch(info);
            _rigidbody.linearVelocity = info.Direction.normalized * info.Speed;
        }

        protected override void Update()
        {
            base.Update();
            if (!_isLaunched) return;
            // R2: one raycast per frame, bounded by deltaTime
            Vector3 from = transform.position;
            float dist = _info.Speed * Time.deltaTime + 0.05f;
            if (Physics.Raycast(from, transform.forward, out var hit, dist, _hitMask, QueryTriggerInteraction.Ignore))
            {
                HandleHit(hit);
            }
        }

        private void HandleHit(RaycastHit hit)
        {
            var info = new DamageInfo
            {
                Amount = _info.Damage,
                Type = _info.DamageType,
                Source = _info.Owner,
                HitPoint = hit.point,
                HitNormal = hit.normal,
                Knockback = 0f
            };
            DamageHandler.TryDamage(hit.collider.gameObject, info);
            Despawn();
        }
    }
}
