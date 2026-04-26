using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Puzzle
{
    /// <summary>Fires events when a matching ColorChannel is reported on it.</summary>
    public sealed class ColorReceiver : MonoBehaviour
    {
        [SerializeField] private ColorChannel _expected;
        [SerializeField] private UnityEvent _onMatched;
        [SerializeField] private UnityEvent _onUnmatched;

        public bool IsMatched { get; private set; }

        public void Notify(ColorChannel actual)
        {
            bool match = actual == _expected;
            if (match == IsMatched) return;
            IsMatched = match;
            if (match) _onMatched?.Invoke();
            else _onUnmatched?.Invoke();
        }
    }
}
