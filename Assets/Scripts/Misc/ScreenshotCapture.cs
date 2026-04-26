using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Misc
{
    /// <summary>Takes a screenshot on key press. For jam screenshots.</summary>
    public sealed class ScreenshotCapture : MonoBehaviour
    {
        [SerializeField] private InputActionReference _captureAction;

        private void OnEnable() { if (_captureAction != null) _captureAction.action.Enable(); }
        private void OnDisable() { if (_captureAction != null) _captureAction.action.Disable(); }

        private void Update()
        {
            if (_captureAction == null) return;
            if (!_captureAction.action.WasPressedThisFrame()) return;
            string filename = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png";
            string path = Path.Combine(Application.persistentDataPath, filename);
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log($"[ScreenshotCapture] saved {path}");
        }
    }
}
