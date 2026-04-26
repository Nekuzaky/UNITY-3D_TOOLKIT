using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace GameJamToolkit.UI
{
    /// <summary>
    /// Dialog box with typing effect. Pushes a list of lines,
    /// advance via ContinueLine. Notifies OnFinished at the end.
    /// </summary>
    public sealed class DialogueBoxUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _root;
        [Min(0f)] [SerializeField] private float _charsPerSecond = 40f;

        private string[] _lines;
        private int _index;
        private Coroutine _typingRoutine;
        private bool _isTyping;
        public event Action OnFinished;

        public void Show(string[] lines)
        {
            Debug.Assert(lines != null && lines.Length > 0, "[DialogueBoxUI.Show] lines is empty"); // R5
            _lines = lines;
            _index = 0;
            if (_root != null) _root.SetActive(true);
            TypeCurrent();
        }

        public void ContinueLine()
        {
            if (_isTyping) { CompleteTyping(); return; }
            _index++;
            if (_index >= (_lines?.Length ?? 0)) { Hide(); return; }
            TypeCurrent();
        }

        public void Hide()
        {
            if (_typingRoutine != null) { StopCoroutine(_typingRoutine); _typingRoutine = null; }
            if (_root != null) _root.SetActive(false);
            OnFinished?.Invoke();
        }

        private void TypeCurrent()
        {
            if (_typingRoutine != null) StopCoroutine(_typingRoutine);
            _typingRoutine = StartCoroutine(TypeRoutine(_lines[_index]));
        }

        private IEnumerator TypeRoutine(string text)
        {
            _isTyping = true;
            if (_label == null) yield break;
            _label.text = string.Empty;
            float charDuration = _charsPerSecond > 0f ? 1f / _charsPerSecond : 0f;
            int max = text.Length; // R2
            for (int i = 0; i < max; i++)
            {
                _label.text += text[i];
                if (charDuration > 0f) yield return new WaitForSeconds(charDuration);
            }
            _isTyping = false;
        }

        private void CompleteTyping()
        {
            if (_typingRoutine != null) StopCoroutine(_typingRoutine);
            if (_label != null && _lines != null && _index < _lines.Length) _label.text = _lines[_index];
            _isTyping = false;
        }
    }
}
