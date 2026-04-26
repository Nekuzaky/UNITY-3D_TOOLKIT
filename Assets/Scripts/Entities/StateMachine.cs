using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>
    /// Generic reusable state machine for entities and other objects.
    /// Not dependent on MonoBehaviour, but can be read by one.
    /// </summary>
    public sealed class StateMachine
    {
        private readonly Dictionary<System.Type, StateBase> _stateDict = new Dictionary<System.Type, StateBase>();
        public StateBase CurrentState { get; private set; }

        public void Register(StateBase state)
        {
            Debug.Assert(state != null, "[StateMachine.Register] state is null"); // R5
            _stateDict[state.GetType()] = state;
        }

        public void TransitionTo<T>() where T : StateBase
        {
            if (!_stateDict.TryGetValue(typeof(T), out var next))
            {
                Debug.LogWarning($"[StateMachine] missing state {typeof(T).Name}");
                return;
            }
            if (next == CurrentState) return;
            CurrentState?.OnExit();
            CurrentState = next;
            CurrentState.OnEnter();
        }

        public void Tick(float dt) { CurrentState?.OnUpdate(dt); }
        public void FixedTick(float fdt) { CurrentState?.OnFixedUpdate(fdt); }
    }
}
