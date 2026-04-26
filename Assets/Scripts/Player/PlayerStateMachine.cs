using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>Generic player state machine. Subclass or populate via an init script.</summary>
    public sealed class PlayerStateMachine : MonoBehaviour
    {
        private readonly Dictionary<System.Type, PlayerStateBase> _stateDict = new Dictionary<System.Type, PlayerStateBase>();
        public PlayerStateBase CurrentState { get; private set; }

        public void Register(PlayerStateBase state)
        {
            Debug.Assert(state != null, "[PlayerStateMachine.Register] state is null"); // R5
            state.Bind(this);
            _stateDict[state.GetType()] = state;
        }

        public void TransitionTo<T>() where T : PlayerStateBase
        {
            if (!_stateDict.TryGetValue(typeof(T), out var next))
            {
                Debug.LogWarning($"[PlayerStateMachine] state not registered: {typeof(T).Name}");
                return;
            }
            if (next == CurrentState) return;
            CurrentState?.OnExit();
            CurrentState = next;
            CurrentState.OnEnter();
        }

        private void Update() { CurrentState?.OnUpdate(Time.deltaTime); }
        private void FixedUpdate() { CurrentState?.OnFixedUpdate(Time.fixedDeltaTime); }
    }
}
