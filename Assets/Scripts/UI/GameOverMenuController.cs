using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    public sealed class GameOverMenuController : MonoBehaviour
    {
        [SerializeField] private string _menuSceneName = "MainMenu";

        public void OnRetry()
        {
            if (SceneLoader.HasInstance) SceneLoader.Instance.ReloadCurrent();
            if (GameManager.HasInstance) GameManager.Instance.StartGameplay();
        }

        public void OnMainMenu()
        {
            if (SceneLoader.HasInstance) SceneLoader.Instance.LoadScene(_menuSceneName);
            if (GameManager.HasInstance) GameManager.Instance.OpenMainMenu();
        }

        public void OnQuit() { AppQuitter.Quit(); }
    }
}
