namespace GameJamToolkit.AI.BehaviorTree
{
    public enum BTStatus { Success, Failure, Running }

    /// <summary>Abstract node of a minimal BehaviorTree.</summary>
    public abstract class BTNode
    {
        public abstract BTStatus Tick();
    }
}
