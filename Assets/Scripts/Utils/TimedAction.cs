using System;
using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Fires an action after a delay. For micro-callbacks without a coroutine.</summary>
    public sealed class TimedAction : MonoBehaviour
    {
        private float _runAt;
        private Action _callback;
        private bool _isActive;

        public void Schedule(float delay, Action callback)
        {
            Debug.Assert(callback != null, "[TimedAction.Schedule] callback is null"); // R5
            Debug.Assert(delay >= 0f, "[TimedAction.Schedule] delay is negative"); // R5
            _runAt = Time.time + Mathf.Max(0f, delay);
            _callback = callback;
            _isActive = true;
        }

        public void Cancel()
        {
            _isActive = false;
            _callback = null;
        }

        private void Update()
        {
            if (!_isActive || Time.time < _runAt) return;
            var cb = _callback;
            _isActive = false;
            _callback = null;
            cb?.Invoke();
        }
    }
}
