using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>Base state of a player StateMachine. No Unity coupling, simple wrapper.</summary>
    public abstract class PlayerStateBase
    {
        protected PlayerStateMachine Machine;
        protected GameObject Owner => Machine != null ? Machine.gameObject : null;

        public void Bind(PlayerStateMachine machine)
        {
            Debug.Assert(machine != null, "[PlayerStateBase.Bind] machine is null"); // R5
            Machine = machine;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnFixedUpdate(float fixedDeltaTime) { }
    }
}
