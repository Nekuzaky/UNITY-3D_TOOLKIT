using UnityEngine;

namespace GameJamToolkit.Quest
{
    /// <summary>Individual quest objective. Subclass for Kill, Collect, Reach.</summary>
    public abstract class QuestObjective : ScriptableObject
    {
        public string Description;
        public int Required = 1;
        [System.NonSerialized] public int Current;
        [System.NonSerialized] public bool IsCompleted;

        public virtual void Reset() { Current = 0; IsCompleted = false; }
        public virtual void AddProgress(int delta)
        {
            Debug.Assert(delta >= 0, "[QuestObjective.AddProgress] delta is negative"); // R5
            if (IsCompleted) return;
            Current = Mathf.Min(Required, Current + delta);
            if (Current >= Required) IsCompleted = true;
        }
    }
}
