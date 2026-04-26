using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace GameJamToolkit.UI
{
    /// <summary>Global tooltip. Scene singleton. Shows text under the mouse.</summary>
    public sealed class TooltipUI : MonoBehaviour
    {
        public static TooltipUI ActiveInstance { get; private set; }
        [SerializeField] private RectTransform _root;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Vector2 _mouseOffset = new Vector2(16, -16);

        private void Awake()
        {
            ActiveInstance = this;
            Hide();
        }

        public void Show(string text)
        {
            if (_root == null || _label == null) return;
            _label.text = text;
            _root.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_root != null) _root.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_root == null || !_root.gameObject.activeSelf) return;
            // R7: Mouse.current can be null on mouse-less platforms
            Vector2 mouse = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
            _root.position = mouse + _mouseOffset;
        }
    }
}
