using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.RTS
{
    /// <summary>
    /// Mouse-based selection: click to pick a single unit, drag to select a
    /// rectangle (screen-space). Filters by team.
    /// </summary>
    public sealed class SelectionManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _clickAction;
        [SerializeField] private LayerMask _selectableMask = ~0;
        [SerializeField] private int _ownTeamId;
        [SerializeField] private float _maxRayDistance = 200f;

        private readonly List<Selectable> _selected = new List<Selectable>();
        private Vector2 _dragStart;
        private bool _isDragging;

        public IReadOnlyList<Selectable> SelectedUnits => _selected;

        private void OnEnable() { if (_clickAction != null) _clickAction.action.Enable(); }
        private void OnDisable() { if (_clickAction != null) _clickAction.action.Disable(); }

        private void Update()
        {
            if (Mouse.current == null || Camera.main == null) return;

            if (_clickAction != null && _clickAction.action.WasPressedThisFrame())
            {
                _dragStart = Mouse.current.position.ReadValue();
                _isDragging = true;
                ClearSelection();
            }
            if (_isDragging && _clickAction != null && _clickAction.action.WasReleasedThisFrame())
            {
                Vector2 dragEnd = Mouse.current.position.ReadValue();
                if ((dragEnd - _dragStart).sqrMagnitude < 16f) PickSingle(dragEnd);
                else PickInRect(_dragStart, dragEnd);
                _isDragging = false;
            }
        }

        private void PickSingle(Vector2 screen)
        {
            Ray ray = Camera.main.ScreenPointToRay(screen);
            if (!Physics.Raycast(ray, out var hit, _maxRayDistance, _selectableMask, QueryTriggerInteraction.Ignore)) return;
            var sel = hit.collider.GetComponentInParent<Selectable>();
            if (sel == null || sel.TeamId != _ownTeamId) return;
            sel.SetSelected(true);
            _selected.Add(sel);
        }

        private void PickInRect(Vector2 a, Vector2 b)
        {
            Rect r = Rect.MinMaxRect(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
            var all = Object.FindObjectsByType<Selectable>(FindObjectsInactive.Exclude);
            int max = all.Length; // R2
            for (int i = 0; i < max; i++)
            {
                if (all[i].TeamId != _ownTeamId) continue;
                Vector3 sp = Camera.main.WorldToScreenPoint(all[i].transform.position);
                if (sp.z < 0f) continue;
                if (r.Contains(sp)) { all[i].SetSelected(true); _selected.Add(all[i]); }
            }
        }

        public void ClearSelection()
        {
            int max = _selected.Count; // R2
            for (int i = 0; i < max; i++) { if (_selected[i] != null) _selected[i].SetSelected(false); }
            _selected.Clear();
        }
    }
}
