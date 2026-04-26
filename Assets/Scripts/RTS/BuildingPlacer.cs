using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.RTS
{
    /// <summary>
    /// Ghost-preview building placer. Move the mouse to position a translucent
    /// preview, click to confirm. Validates ground hit and economy cost.
    /// </summary>
    public sealed class BuildingPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _ghost;
        [SerializeField] private GameObject _buildingPrefab;
        [SerializeField] private ResourceEconomy _economy;
        [SerializeField] private string _costKey = "wood";
        [SerializeField] private int _costAmount = 100;
        [SerializeField] private LayerMask _groundMask = ~0;
        [SerializeField] private InputActionReference _confirmAction;

        public bool IsActive { get; private set; }

        public void Begin()
        {
            IsActive = true;
            if (_ghost != null) _ghost.SetActive(true);
        }

        public void Cancel()
        {
            IsActive = false;
            if (_ghost != null) _ghost.SetActive(false);
        }

        private void OnEnable() { if (_confirmAction != null) _confirmAction.action.Enable(); }
        private void OnDisable() { if (_confirmAction != null) _confirmAction.action.Disable(); }

        private void Update()
        {
            if (!IsActive || Camera.main == null || Mouse.current == null) return;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit, 200f, _groundMask, QueryTriggerInteraction.Ignore)) return;
            if (_ghost != null) _ghost.transform.position = hit.point;

            if (_confirmAction != null && _confirmAction.action.WasPressedThisFrame())
            {
                if (_economy != null && !_economy.TrySpend(_costKey, _costAmount)) return;
                if (_buildingPrefab != null) Instantiate(_buildingPrefab, hit.point, Quaternion.identity); // R3 exempt: build commit
                Cancel();
            }
        }
    }
}
