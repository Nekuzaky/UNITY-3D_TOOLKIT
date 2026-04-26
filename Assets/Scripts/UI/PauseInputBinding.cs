using UnityEngine;
using UnityEngine.InputSystem;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    /// <summary>Maps an InputAction to GameManager.TogglePause.</summary>
    public sealed class PauseInputBinding : MonoBehaviour
    {
        [SerializeField] private InputActionReference _pauseAction;

        private void OnEnable()
        {
            Debug.Assert(_pauseAction != null, "[PauseInputBinding] _pauseAction is null"); // R5
            if (_pauseAction == null) return;
            _pauseAction.action.Enable();
            _pauseAction.action.performed += HandlePause;
        }

        private void OnDisable()
        {
            if (_pauseAction == null) return;
            _pauseAction.action.performed -= HandlePause;
            _pauseAction.action.Disable();
        }

        private void HandlePause(InputAction.CallbackContext ctx)
        {
            if (GameManager.HasInstance) GameManager.Instance.TogglePause();
        }
    }
}
