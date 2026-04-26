using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Combat
{
    /// <summary>Ballistic projectile that explodes in an area on timeout or contact.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class GrenadeProjectile : ProjectileBase
    {
        [SerializeField] private float _explosionRadius = 4f;
        [SerializeField] private string _vfxPoolKey = "Explosion";
        [SerializeField] private bool _explodeOnContact = true;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = true;
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
            if (Time.time - _spawnTime >= _info.Lifetime) Explode();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_explodeOnContact) Explode();
        }

        private void Explode()
        {
            // R3 exempt: VFX spawned via pool
            if (!string.IsNullOrEmpty(_vfxPoolKey) && ObjectPool.HasInstance)
            {
                ObjectPool.Instance.Get(_vfxPoolKey, transform.position, Quaternion.identity);
            }

            var hits = Physics.OverlapSphere(transform.position, _explosionRadius, _hitMask);
            int max = hits.Length; // R2 borne fixe
            for (int i = 0; i < max; i++)
            {
                var col = hits[i];
                if (col == null) continue;
                Vector3 hitPoint = col.ClosestPoint(transform.position);
                float falloff = 1f - Mathf.Clamp01(Vector3.Distance(transform.position, hitPoint) / _explosionRadius);
                DamageHandler.TryDamage(col.gameObject, new DamageInfo
                {
                    Amount = _info.Damage * falloff,
                    Type = _info.DamageType,
                    Source = _info.Owner,
                    HitPoint = hitPoint,
                    HitNormal = (hitPoint - transform.position).normalized,
                    Knockback = falloff * 6f
                });
            }
            Despawn();
        }
    }
}
