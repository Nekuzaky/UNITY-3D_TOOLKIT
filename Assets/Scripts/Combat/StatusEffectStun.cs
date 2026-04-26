using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Stun: disables a configurable MonoBehaviour control.
    /// We attach and list the components to disable.
    /// </summary>
    public sealed class StatusEffectStun : StatusEffectBase
    {
        [SerializeField] private MonoBehaviour[] _toDisable;

        private bool _wasDisabled;

        protected override void OnApply()
        {
            if (_toDisable == null || _wasDisabled) return;
            // R2: bounded by array length
            int max = _toDisable.Length;
            for (int i = 0; i < max; i++)
            {
                if (_toDisable[i] != null) _toDisable[i].enabled = false;
            }
            _wasDisabled = true;
        }

        protected override void OnExpire()
        {
            if (_toDisable == null || !_wasDisabled) return;
            int max = _toDisable.Length;
            for (int i = 0; i < max; i++)
            {
                if (_toDisable[i] != null) _toDisable[i].enabled = true;
            }
            _wasDisabled = false;
        }
    }
}
