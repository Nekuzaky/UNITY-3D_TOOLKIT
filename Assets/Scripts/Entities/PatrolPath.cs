using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>Visualizable waypoint list in the scene. Plug into an AI.</summary>
    public sealed class PatrolPath : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        public Transform[] Points => _points;

        private void OnDrawGizmos()
        {
            if (_points == null) return;
            Gizmos.color = Color.yellow;
            int max = _points.Length;
            for (int i = 0; i < max; i++)
            {
                if (_points[i] == null) continue;
                Gizmos.DrawWireSphere(_points[i].position, 0.2f);
                if (i + 1 < max && _points[i + 1] != null) Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            }
        }
    }
}
