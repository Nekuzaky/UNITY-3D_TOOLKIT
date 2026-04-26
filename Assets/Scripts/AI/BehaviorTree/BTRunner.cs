using UnityEngine;

namespace GameJamToolkit.AI.BehaviorTree
{
    /// <summary>Ticks a BTNode root every frame. Override to supply the tree.</summary>
    public abstract class BTRunner : MonoBehaviour
    {
        protected BTNode _root;

        protected virtual void Start() { _root = BuildTree(); }
        protected virtual void Update() { _root?.Tick(); }
        protected abstract BTNode BuildTree();
    }
}
