using UnityEngine;
using UnityEngine.InputSystem;
using GameJamToolkit.Core;

namespace GameJamToolkit.Misc
{
    /// <summary>Minimal debug menu. Toggle with an InputAction (F1).</summary>
    public sealed class DebugMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private InputActionReference _toggleAction;

        private void Awake()
        {
            if (_root != null) _root.SetActive(false);
        }

        private void OnEnable() { if (_toggleAction != null) _toggleAction.action.Enable(); }
        private void OnDisable() { if (_toggleAction != null) _toggleAction.action.Disable(); }

        private void Update()
        {
            if (_toggleAction == null || _root == null) return;
            if (_toggleAction.action.WasPressedThisFrame()) _root.SetActive(!_root.activeSelf);
        }

        public void OnRestartScene() { if (SceneLoader.HasInstance) SceneLoader.Instance.ReloadCurrent(); }
        public void OnGiveScore(int amount) { if (ScoreManager.HasInstance) ScoreManager.Instance.Add(amount); }
        public void OnTogglePause() { if (GameManager.HasInstance) GameManager.Instance.TogglePause(); }
    }
}
