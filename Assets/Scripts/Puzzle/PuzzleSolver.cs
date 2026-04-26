using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Puzzle
{
    /// <summary>Aggregates puzzle conditions; fires OnSolved when all are true.</summary>
    public sealed class PuzzleSolver : MonoBehaviour
    {
        [SerializeField] private bool[] _conditions;
        [SerializeField] private UnityEvent _onSolved;
        [SerializeField] private UnityEvent _onUnsolved;

        public bool IsSolved { get; private set; }

        public void SetCondition(int index, bool value)
        {
            Debug.Assert(_conditions != null, "[PuzzleSolver.SetCondition] _conditions is null"); // R5
            Debug.Assert(index >= 0 && index < _conditions.Length, "[PuzzleSolver.SetCondition] index out of range"); // R5
            if (_conditions == null) return;
            _conditions[index] = value;
            UpdateState();
        }

        private void UpdateState()
        {
            bool all = true;
            int max = _conditions.Length; // R2
            for (int i = 0; i < max; i++) { if (!_conditions[i]) { all = false; break; } }
            if (all == IsSolved) return;
            IsSolved = all;
            if (all) _onSolved?.Invoke();
            else _onUnsolved?.Invoke();
        }
    }
}
