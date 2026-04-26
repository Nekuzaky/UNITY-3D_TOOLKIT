using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Static damage zone (lava, spikes, gas). Tick at a fixed interval.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class DamageZone : MonoBehaviour
    {
        [SerializeField] private float _damagePerTick = 5f;
        [SerializeField] private DamageType _type = DamageType.Physical;
        [Min(0.05f)] [SerializeField] private float _tickInterval = 0.5f;
        [SerializeField] private LayerMask _targetMask = ~0;

        private float _nextTick;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (Time.time < _nextTick) return;
            if (((1 << other.gameObject.layer) & _targetMask) == 0) return;
            DamageHandler.TryDamage(other.gameObject, new DamageInfo
            {
                Amount = _damagePerTick,
                Type = _type,
                Source = gameObject,
                HitPoint = other.ClosestPoint(transform.position),
                HitNormal = Vector3.up
            });
            _nextTick = Time.time + _tickInterval;
        }
    }
}
