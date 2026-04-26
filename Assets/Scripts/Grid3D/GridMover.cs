using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Grid3D
{
    /// <summary>Moves a GameObject along a path of GridCoord, snapping to cell centers.</summary>
    public sealed class GridMover : MonoBehaviour
    {
        [SerializeField] private GridSettings _settings;
        [SerializeField] private GridOccupancy _occupancy;
        [SerializeField] private float _stepSeconds = 0.18f;

        public bool IsMoving { get; private set; }
        public event System.Action OnArrived;

        public GridCoord CurrentCoord { get; private set; }

        public void Snap(GridCoord c)
        {
            CurrentCoord = c;
            if (_settings != null) transform.position = _settings.ToWorld(c);
            if (_occupancy != null) _occupancy.TryOccupy(c, gameObject);
        }

        public void Follow(List<GridCoord> path)
        {
            Debug.Assert(_settings != null, "[GridMover.Follow] _settings is null"); // R5
            Debug.Assert(path != null, "[GridMover.Follow] path is null"); // R5
            if (IsMoving || path == null || path.Count == 0) return;
            StartCoroutine(FollowRoutine(path));
        }

        private IEnumerator FollowRoutine(List<GridCoord> path)
        {
            IsMoving = true;
            int max = path.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var next = path[i];
                Vector3 from = transform.position;
                Vector3 to = _settings.ToWorld(next);
                float t = 0f;
                while (t < _stepSeconds)
                {
                    t += Time.deltaTime;
                    transform.position = Vector3.Lerp(from, to, Mathf.Clamp01(t / _stepSeconds));
                    yield return null;
                }
                if (_occupancy != null) { _occupancy.FreeOf(gameObject); _occupancy.TryOccupy(next, gameObject); }
                CurrentCoord = next;
            }
            IsMoving = false;
            OnArrived?.Invoke();
        }
    }
}
