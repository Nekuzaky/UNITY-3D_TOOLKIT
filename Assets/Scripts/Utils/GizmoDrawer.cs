using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Level-design helper: draws a zone visible in the editor.</summary>
    public sealed class GizmoDrawer : MonoBehaviour
    {
        public enum Shape { Sphere, Box, WireSphere, WireBox }
        [SerializeField] private Shape _shape = Shape.WireSphere;
        [SerializeField] private Color _color = Color.cyan;
        [SerializeField] private Vector3 _size = Vector3.one;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private bool _onlyWhenSelected = false;

        private void OnDrawGizmos() { if (!_onlyWhenSelected) DrawShape(); }
        private void OnDrawGizmosSelected() { if (_onlyWhenSelected) DrawShape(); }

        private void DrawShape()
        {
            Gizmos.color = _color;
            switch (_shape)
            {
                case Shape.Sphere: Gizmos.DrawSphere(transform.position, _radius); break;
                case Shape.Box: Gizmos.DrawCube(transform.position, _size); break;
                case Shape.WireSphere: Gizmos.DrawWireSphere(transform.position, _radius); break;
                case Shape.WireBox: Gizmos.DrawWireCube(transform.position, _size); break;
            }
        }
    }
}
