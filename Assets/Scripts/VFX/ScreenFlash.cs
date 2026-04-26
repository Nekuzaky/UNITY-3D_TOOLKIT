using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamToolkit.VFX
{
    /// <summary>Full-screen flash (UI Image full alpha then fade-out). Good for big impacts.</summary>
    public sealed class ScreenFlash : MonoBehaviour
    {
        public static ScreenFlash ActiveInstance { get; private set; }
        [SerializeField] private Image _image;
        [SerializeField] private float _defaultSeconds = 0.25f;

        private Coroutine _routine;

        private void Awake() { ActiveInstance = this; if (_image != null) _image.color = new Color(1, 1, 1, 0); }

        public void Flash(Color color, float duration = -1f)
        {
            if (_image == null) return;
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(FlashRoutine(color, duration > 0f ? duration : _defaultSeconds));
        }

        private IEnumerator FlashRoutine(Color color, float duration)
        {
            float t = 0f;
            // R2: bounded by duration
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(1f, 0f, t / duration);
                _image.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
            _image.color = new Color(color.r, color.g, color.b, 0f);
        }
    }
}
