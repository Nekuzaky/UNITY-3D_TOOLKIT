using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Items
{
    /// <summary>Quick slot selection 0..N as number keys or scroll.</summary>
    public sealed class HotbarController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [Min(1)] [SerializeField] private int _hotbarSize = 8;
        [SerializeField] private InputActionReference _selectAction;

        public int CurrentIndex { get; private set; }
        public event System.Action<int> OnIndexChanged;

        private void OnEnable() { if (_selectAction != null) _selectAction.action.Enable(); }
        private void OnDisable() { if (_selectAction != null) _selectAction.action.Disable(); }

        private void Update()
        {
            if (_selectAction == null) return;
            float v = _selectAction.action.ReadValue<float>();
            if (Mathf.Abs(v) < 0.1f) return;
            int dir = v > 0f ? -1 : 1;
            CurrentIndex = (CurrentIndex + dir + _hotbarSize) % _hotbarSize;
            OnIndexChanged?.Invoke(CurrentIndex);
        }
    }
}
