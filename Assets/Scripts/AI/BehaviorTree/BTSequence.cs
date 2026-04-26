using System.Collections.Generic;

namespace GameJamToolkit.AI.BehaviorTree
{
    /// <summary>Runs children sequentially; stops on first Failure or Running.</summary>
    public sealed class BTSequence : BTNode
    {
        private readonly List<BTNode> _children = new List<BTNode>();

        public BTSequence Add(BTNode node) { _children.Add(node); return this; }

        public override BTStatus Tick()
        {
            int max = _children.Count; // R2
            for (int i = 0; i < max; i++)
            {
                BTStatus s = _children[i].Tick();
                if (s != BTStatus.Success) return s;
            }
            return BTStatus.Success;
        }
    }
}
