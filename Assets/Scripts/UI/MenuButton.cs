using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameJamToolkit.UI
{
    /// <summary>UI button with a UnityEvent for inspector wiring.</summary>
    [RequireComponent(typeof(Button))]
    public sealed class MenuButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onClick;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            Debug.Assert(_button != null, "[MenuButton] _button null"); // R5
        }

        private void OnEnable() { if (_button != null) _button.onClick.AddListener(HandleClick); }
        private void OnDisable() { if (_button != null) _button.onClick.RemoveListener(HandleClick); }
        private void HandleClick() { _onClick?.Invoke(); }
    }
}
