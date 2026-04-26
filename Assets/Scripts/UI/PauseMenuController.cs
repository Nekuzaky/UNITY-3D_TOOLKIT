using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    public sealed class PauseMenuController : MonoBehaviour
    {
        public void OnResume() { if (GameManager.HasInstance) GameManager.Instance.TogglePause(); }
        public void OnRestart()
        {
            if (SceneLoader.HasInstance) SceneLoader.Instance.ReloadCurrent();
            if (GameManager.HasInstance) GameManager.Instance.StartGameplay();
        }
        public void OnMainMenu(string sceneName)
        {
            if (SceneLoader.HasInstance) SceneLoader.Instance.LoadScene(sceneName);
            if (GameManager.HasInstance) GameManager.Instance.OpenMainMenu();
        }
        public void OnQuit() { AppQuitter.Quit(); }
    }
}
