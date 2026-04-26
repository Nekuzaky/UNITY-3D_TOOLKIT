using System;

namespace GameJamToolkit.AI.BehaviorTree
{
    /// <summary>Boolean condition. Success if true, Failure otherwise.</summary>
    public sealed class BTCondition : BTNode
    {
        private readonly Func<bool> _predicate;
        public BTCondition(Func<bool> predicate) { _predicate = predicate; }
        public override BTStatus Tick() => _predicate != null && _predicate() ? BTStatus.Success : BTStatus.Failure;
    }
}
