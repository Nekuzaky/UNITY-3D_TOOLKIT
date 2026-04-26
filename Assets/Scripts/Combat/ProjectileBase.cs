using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Abstract base for any pooled projectile. Manages lifetime + return-to-pool.
    /// Derived classes implement movement and collision.
    /// </summary>
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected LayerMask _hitMask = ~0;

        protected ProjectileLaunchInfo _info;
        protected float _spawnTime;
        protected bool _isLaunched;

        public virtual void Launch(ProjectileLaunchInfo info)
        {
            Debug.Assert(info.Speed >= 0f, "[ProjectileBase.Launch] Speed is negative"); // R5
            Debug.Assert(info.Lifetime > 0f, "[ProjectileBase.Launch] Lifetime <= 0"); // R5
            _info = info;
            _spawnTime = Time.time;
            _isLaunched = true;
            transform.forward = info.Direction.sqrMagnitude > 0.001f ? info.Direction.normalized : transform.forward;
        }

        protected virtual void Update()
        {
            if (!_isLaunched) return;
            if (Time.time - _spawnTime >= _info.Lifetime) Despawn();
        }

        protected void Despawn()
        {
            _isLaunched = false;
            if (!ObjectPool.HasInstance) { Destroy(gameObject); return; }
            ObjectPool.Instance.Return(_info.PoolKey, gameObject);
        }
    }
}
