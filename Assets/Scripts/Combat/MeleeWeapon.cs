using System.Collections;
using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Melee weapon: activates a child hitbox for a short interval.
    /// </summary>
    public sealed class MeleeWeapon : WeaponBase
    {
        [SerializeField] private HitboxController _hitbox;
        [Min(0f)] [SerializeField] private float _activeDuration = 0.18f;

        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(_hitbox != null, "[MeleeWeapon] _hitbox is null"); // R5
            if (_hitbox != null) _hitbox.Deactivate();
        }

        protected override void FireInternal()
        {
            if (_hitbox == null) return;
            StartCoroutine(SwingRoutine());
        }

        private IEnumerator SwingRoutine()
        {
            _hitbox.Activate();
            yield return new WaitForSeconds(_activeDuration);
            _hitbox.Deactivate();
        }
    }
}
