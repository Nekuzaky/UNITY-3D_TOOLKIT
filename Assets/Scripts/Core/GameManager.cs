using UnityEngine;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Global singleton owning the game state. The only place allowed to drive
    /// GameState transitions. State changes are broadcast through
    /// <see cref="EventBus"/> as <see cref="GameStateChangedEvent"/>.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public sealed class GameManager : PersistentSingleton<GameManager>
    {
        [Header("Initial state")]
        [SerializeField] private GameState _initialState = GameState.Boot;
        [SerializeField] private bool _autoStart = true;

        public GameState CurrentState { get; private set; }
        public GameState PreviousState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CurrentState = GameState.Boot;
            PreviousState = GameState.Boot;
        }

        private void Start()
        {
            if (_autoStart) ChangeState(_initialState);
        }

        /// <summary>Transition to <paramref name="newState"/>. Ignored if already current.</summary>
        public void ChangeState(GameState newState)
        {
            Debug.Assert(newState != GameState.Boot || CurrentState == GameState.Boot, "[GameManager] cannot re-enter Boot"); // R5
            if (newState == CurrentState) return; // R5

            PreviousState = CurrentState;
            CurrentState = newState;
            HandleStateEntry(newState);
            EventBus.Publish(new GameStateChangedEvent { Previous = PreviousState, Current = CurrentState });
        }

        public void StartGameplay() => ChangeState(GameState.Gameplay);
        public void OpenMainMenu() => ChangeState(GameState.MainMenu);
        public void TriggerGameOver() => ChangeState(GameState.GameOver);
        public void TriggerWin() => ChangeState(GameState.Win);

        /// <summary>Pause / resume. Adjusts Time.timeScale and publishes a PauseToggledEvent.</summary>
        public void TogglePause()
        {
            if (CurrentState == GameState.Gameplay)
            {
                ChangeState(GameState.Paused);
                Time.timeScale = 0f;
                EventBus.Publish(new PauseToggledEvent { IsPaused = true });
            }
            else if (CurrentState == GameState.Paused)
            {
                ChangeState(GameState.Gameplay);
                Time.timeScale = 1f;
                EventBus.Publish(new PauseToggledEvent { IsPaused = false });
            }
        }

        private void HandleStateEntry(GameState state)
        {
            // R5: keep timeScale consistent with the new state
            switch (state)
            {
                case GameState.Paused: Time.timeScale = 0f; break;
                case GameState.Gameplay: Time.timeScale = 1f; break;
                case GameState.GameOver: Time.timeScale = 0f; break;
                case GameState.Win: Time.timeScale = 0f; break;
                default: Time.timeScale = 1f; break;
            }
        }
    }
}
