using UnityEngine;
using GameJamToolkit.Utils;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Abstract base for any weapon. Encapsulates cooldown, owner, anchor.
    /// Derived classes implement FireInternal with the specific logic.
    /// </summary>
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _owner;
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected float _damage = 10f;
        [SerializeField] protected float _cooldownSeconds = 0.3f;
        [SerializeField] protected DamageType _damageType = DamageType.Physical;

        protected Cooldown _cooldown;

        protected virtual void Awake()
        {
            Debug.Assert(_damage > 0f, "[WeaponBase.Awake] _damage <= 0"); // R5
            Debug.Assert(_cooldownSeconds >= 0f, "[WeaponBase.Awake] _cooldownSeconds < 0"); // R5
            _cooldown = Cooldown.Of(_cooldownSeconds);
            if (_muzzle == null) _muzzle = transform;
            if (_owner == null) _owner = transform.root.gameObject;
        }

        public bool CanFire => _cooldown.IsReady;

        public bool TryFire()
        {
            if (!_cooldown.TryConsume()) return false;
            FireInternal();
            return true;
        }

        protected abstract void FireInternal();

        public void SetOwner(GameObject owner) { _owner = owner; }
        public void SetDamage(float value) { _damage = Mathf.Max(0f, value); }
    }
}
