using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Puzzle
{
    /// <summary>
    /// Multi-condition lock: fires OnUnlocked once all _requiredCount inputs
    /// are reported active via NotifyConditionMet.
    /// </summary>
    public sealed class LockChain : MonoBehaviour
    {
        [SerializeField] private int _requiredCount = 3;
        [SerializeField] private UnityEvent _onUnlocked;
        [SerializeField] private UnityEvent _onLocked;

        private int _activeCount;
        public bool IsUnlocked { get; private set; }

        public void NotifyConditionMet()
        {
            _activeCount = Mathf.Min(_requiredCount, _activeCount + 1);
            if (_activeCount >= _requiredCount && !IsUnlocked)
            {
                IsUnlocked = true;
                _onUnlocked?.Invoke();
            }
        }

        public void NotifyConditionLost()
        {
            _activeCount = Mathf.Max(0, _activeCount - 1);
            if (_activeCount < _requiredCount && IsUnlocked)
            {
                IsUnlocked = false;
                _onLocked?.Invoke();
            }
        }
    }
}
