using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Zone that heals IHealable targets at a fixed interval.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class HealZone : MonoBehaviour
    {
        [SerializeField] private float _healPerTick = 5f;
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
            var healable = other.GetComponentInParent<IHealable>();
            if (healable == null) return;
            healable.Heal(_healPerTick);
            _nextTick = Time.time + _tickInterval;
        }
    }
}
