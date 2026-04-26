using UnityEngine;

namespace GameJamToolkit.AI
{
    /// <summary>Shared waypoint sequence. A follower subscribes to it.</summary>
    public sealed class WaypointPath : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private bool _loop = true;

        public int Count => _points != null ? _points.Length : 0;
        public bool Loop => _loop;

        public Vector3 GetPoint(int index)
        {
            Debug.Assert(_points != null && _points.Length > 0, "[WaypointPath.GetPoint] _points is empty"); // R5
            int safe = ((index % _points.Length) + _points.Length) % _points.Length;
            return _points[safe].position;
        }

        public int Next(int current)
        {
            int n = _points.Length;
            int next = current + 1;
            if (next >= n) return _loop ? 0 : n - 1;
            return next;
        }

        private void OnDrawGizmos()
        {
            if (_points == null) return;
            Gizmos.color = Color.cyan;
            int max = _points.Length;
            for (int i = 0; i < max; i++)
            {
                if (_points[i] == null) continue;
                Gizmos.DrawWireSphere(_points[i].position, 0.25f);
                int n = _loop ? (i + 1) % max : i + 1;
                if (n < max && _points[n] != null) Gizmos.DrawLine(_points[i].position, _points[n].position);
            }
        }
    }
}
