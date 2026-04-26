using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    public sealed class WinMenuController : MonoBehaviour
    {
        [SerializeField] private string _nextSceneName;
        [SerializeField] private string _menuSceneName = "MainMenu";

        public void OnNext()
        {
            if (string.IsNullOrEmpty(_nextSceneName)) return;
            if (SceneLoader.HasInstance) SceneLoader.Instance.LoadScene(_nextSceneName);
            if (GameManager.HasInstance) GameManager.Instance.StartGameplay();
        }

        public void OnMainMenu()
        {
            if (SceneLoader.HasInstance) SceneLoader.Instance.LoadScene(_menuSceneName);
            if (GameManager.HasInstance) GameManager.Instance.OpenMainMenu();
        }
    }
}
