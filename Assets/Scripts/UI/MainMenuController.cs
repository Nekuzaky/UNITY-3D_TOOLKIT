using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    /// <summary>Standard main-menu buttons: Play / Quit.</summary>
    public sealed class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string _gameplaySceneName = "Gameplay";

        public void OnPlay()
        {
            if (GameManager.HasInstance) GameManager.Instance.ChangeState(GameState.Loading);
            if (SceneLoader.HasInstance) SceneLoader.Instance.LoadScene(_gameplaySceneName);
        }

        public void OnQuit() { AppQuitter.Quit(); }

        public void OnSettings()
        {
            // hook: open a sub-menu in SettingsMenuController
        }
    }
}
