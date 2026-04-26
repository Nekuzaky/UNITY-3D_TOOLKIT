using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameJamToolkit.UI
{
    /// <summary>Full-screen fade in/out as an Image alpha 0..1.</summary>
    public sealed class ScreenFader : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _defaultDuration = 0.4f;

        private Coroutine _routine;

        private void Awake()
        {
            Debug.Assert(_fadeImage != null, "[ScreenFader] _fadeImage null"); // R5
            if (_fadeImage != null) _fadeImage.raycastTarget = false;
        }

        public void FadeIn(float duration = -1f) { StartFade(0f, duration); }
        public void FadeOut(float duration = -1f) { StartFade(1f, duration); }

        private void StartFade(float targetAlpha, float duration)
        {
            if (_fadeImage == null) return;
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(FadeRoutine(targetAlpha, duration > 0f ? duration : _defaultDuration));
        }

        private IEnumerator FadeRoutine(float target, float duration)
        {
            Color c = _fadeImage.color;
            float startAlpha = c.a;
            float t = 0f;
            // R2: bounded by duration
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float k = duration > 0f ? Mathf.Clamp01(t / duration) : 1f;
                c.a = Mathf.Lerp(startAlpha, target, k);
                _fadeImage.color = c;
                yield return null;
            }
            c.a = target;
            _fadeImage.color = c;
        }
    }
}
