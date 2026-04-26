using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Homing projectile. Finds the closest target inside a radius.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class HomingProjectile : ProjectileBase
    {
        [SerializeField] private float _searchRadius = 8f;
        [SerializeField] private float _turnRate = 240f;
        [SerializeField] private string _targetTag = "Enemy";

        private Rigidbody _rigidbody;
        private Transform _target;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        public override void Launch(ProjectileLaunchInfo info)
        {
            base.Launch(info);
            _target = FindClosest();
            _rigidbody.linearVelocity = info.Direction.normalized * info.Speed;
        }

        protected override void Update()
        {
            base.Update();
            if (!_isLaunched) return;
            if (_target == null) _target = FindClosest();
            if (_target != null) Steer();
            _rigidbody.linearVelocity = transform.forward * _info.Speed;
        }

        private Transform FindClosest()
        {
            var taggedArray = GameObject.FindGameObjectsWithTag(_targetTag);
            if (taggedArray == null || taggedArray.Length == 0) return null;
            // R2: bounded by array size
            float bestDist = _searchRadius * _searchRadius;
            Transform best = null;
            int max = taggedArray.Length;
            for (int i = 0; i < max; i++)
            {
                var go = taggedArray[i];
                if (go == null) continue;
                float d = (go.transform.position - transform.position).sqrMagnitude;
                if (d < bestDist) { bestDist = d; best = go.transform; }
            }
            return best;
        }

        private void Steer()
        {
            Vector3 toTarget = (_target.position - transform.position).normalized;
            Quaternion goal = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, _turnRate * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _hitMask) == 0) return;
            DamageHandler.TryDamage(other.gameObject, new DamageInfo
            {
                Amount = _info.Damage,
                Type = _info.DamageType,
                Source = _info.Owner,
                HitPoint = other.ClosestPoint(transform.position),
                HitNormal = -transform.forward
            });
            Despawn();
        }
    }
}
