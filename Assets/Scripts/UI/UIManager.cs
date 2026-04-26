using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.UI
{
    /// <summary>Switches UI screens per GameState. Centralizes panel toggling.</summary>
    [DefaultExecutionOrder(-50)]
    public sealed class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject _hudPanel;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _loadingPanel;

        protected override void Awake()
        {
            base.Awake();
            HideAll();
        }

        private void OnEnable() { EventBus.Subscribe<GameStateChangedEvent>(HandleStateChanged); }
        private void OnDisable() { EventBus.Unsubscribe<GameStateChangedEvent>(HandleStateChanged); }

        private void HandleStateChanged(GameStateChangedEvent evt)
        {
            HideAll();
            switch (evt.Current)
            {
                case GameState.MainMenu: SetActive(_mainMenuPanel); break;
                case GameState.Loading: SetActive(_loadingPanel); break;
                case GameState.Gameplay: SetActive(_hudPanel); break;
                case GameState.Paused: SetActive(_hudPanel); SetActive(_pausePanel); break;
                case GameState.GameOver: SetActive(_gameOverPanel); break;
                case GameState.Win: SetActive(_winPanel); break;
                default: break;
            }
        }

        private void HideAll()
        {
            SetActive(_hudPanel, false);
            SetActive(_mainMenuPanel, false);
            SetActive(_pausePanel, false);
            SetActive(_gameOverPanel, false);
            SetActive(_winPanel, false);
            SetActive(_loadingPanel, false);
        }

        private static void SetActive(GameObject go, bool value = true)
        {
            if (go != null) go.SetActive(value);
        }
    }
}
