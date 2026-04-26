using System;

namespace GameJamToolkit.AI.BehaviorTree
{
    /// <summary>Leaf action: delegates the tick to a provided lambda.</summary>
    public sealed class BTAction : BTNode
    {
        private readonly Func<BTStatus> _action;
        public BTAction(Func<BTStatus> action) { _action = action; }
        public override BTStatus Tick() => _action != null ? _action() : BTStatus.Failure;
    }
}
