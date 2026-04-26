using System.Collections.Generic;

namespace GameJamToolkit.AI.BehaviorTree
{
    /// <summary>Runs children until the first Success or Running.</summary>
    public sealed class BTSelector : BTNode
    {
        private readonly List<BTNode> _children = new List<BTNode>();

        public BTSelector Add(BTNode node) { _children.Add(node); return this; }

        public override BTStatus Tick()
        {
            int max = _children.Count; // R2
            for (int i = 0; i < max; i++)
            {
                BTStatus s = _children[i].Tick();
                if (s != BTStatus.Failure) return s;
            }
            return BTStatus.Failure;
        }
    }
}
