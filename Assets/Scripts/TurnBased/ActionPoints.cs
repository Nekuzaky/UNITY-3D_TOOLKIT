using System;
using UnityEngine;

namespace GameJamToolkit.TurnBased
{
    /// <summary>Per-turn action points. Reset on BeginTurn.</summary>
    public sealed class ActionPoints : MonoBehaviour
    {
        [SerializeField] private int _maxPoints = 2;
        public int Current { get; private set; }
        public int Max => _maxPoints;
        public event Action<int, int> OnChanged;

        public void RestoreAll()
        {
            Current = _maxPoints;
            OnChanged?.Invoke(Current, _maxPoints);
        }

        public bool TrySpend(int amount)
        {
            Debug.Assert(amount >= 0, "[ActionPoints.TrySpend] amount is negative"); // R5
            if (Current < amount) return false;
            Current -= amount;
            OnChanged?.Invoke(Current, _maxPoints);
            return true;
        }
    }
}
