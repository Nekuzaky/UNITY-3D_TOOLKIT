using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GameJamToolkit.RTS
{
    /// <summary>Right-click to order selected units to move via NavMeshAgent.</summary>
    public sealed class OrderHandler : MonoBehaviour
    {
        [SerializeField] private SelectionManager _selection;
        [SerializeField] private InputActionReference _orderAction;
        [SerializeField] private LayerMask _groundMask = ~0;
        [SerializeField] private float _maxRayDistance = 200f;
        [SerializeField] private float _formationSpacing = 1.5f;

        private void OnEnable() { if (_orderAction != null) _orderAction.action.Enable(); }
        private void OnDisable() { if (_orderAction != null) _orderAction.action.Disable(); }

        private void Update()
        {
            if (_orderAction == null || _selection == null || Camera.main == null || Mouse.current == null) return;
            if (!_orderAction.action.WasPressedThisFrame()) return;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit, _maxRayDistance, _groundMask, QueryTriggerInteraction.Ignore)) return;
            DispatchMoveOrder(hit.point);
        }

        private void DispatchMoveOrder(Vector3 worldPos)
        {
            var list = _selection.SelectedUnits;
            int max = list.Count; // R2
            int side = Mathf.CeilToInt(Mathf.Sqrt(max));
            for (int i = 0; i < max; i++)
            {
                int row = i / side;
                int col = i % side;
                Vector3 offset = new Vector3((col - side * 0.5f) * _formationSpacing, 0f, (row - side * 0.5f) * _formationSpacing);
                var agent = list[i].GetComponent<NavMeshAgent>();
                if (agent != null && agent.isOnNavMesh) agent.SetDestination(worldPos + offset);
            }
        }
    }
}
