using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameJamToolkit.UI
{
    /// <summary>Queue of transient notifications shown one after another.</summary>
    public sealed class NotificationUI : MonoBehaviour
    {
        public static NotificationUI ActiveInstance { get; private set; }
        [SerializeField] private TMP_Text _label;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _displaySeconds = 2f;
        [SerializeField] private float _fadeSeconds = 0.3f;

        private readonly Queue<string> _queue = new Queue<string>();
        private bool _isShowing;

        private void Awake() { ActiveInstance = this; if (_group != null) _group.alpha = 0f; }

        public void Push(string message)
        {
            Debug.Assert(!string.IsNullOrEmpty(message), "[NotificationUI.Push] message is empty"); // R5
            _queue.Enqueue(message);
            if (!_isShowing) StartCoroutine(ProcessQueue());
        }

        private IEnumerator ProcessQueue()
        {
            _isShowing = true;
            // R2: bounded by taille du queue
            int safety = 1024;
            while (_queue.Count > 0 && safety-- > 0)
            {
                string msg = _queue.Dequeue();
                if (_label != null) _label.text = msg;
                yield return Fade(0f, 1f, _fadeSeconds);
                yield return new WaitForSeconds(_displaySeconds);
                yield return Fade(1f, 0f, _fadeSeconds);
            }
            _isShowing = false;
        }

        private IEnumerator Fade(float from, float to, float duration)
        {
            if (_group == null) yield break;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                _group.alpha = Mathf.Lerp(from, to, t / duration);
                yield return null;
            }
            _group.alpha = to;
        }
    }
}
