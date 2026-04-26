using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Clamps the camera position inside a min/max box.</summary>
    public sealed class CameraBounds : MonoBehaviour
    {
        [SerializeField] private Vector3 _min = new Vector3(-50f, 0f, -50f);
        [SerializeField] private Vector3 _max = new Vector3(50f, 30f, 50f);

        private void LateUpdate()
        {
            Vector3 p = transform.position;
            p.x = Mathf.Clamp(p.x, _min.x, _max.x);
            p.y = Mathf.Clamp(p.y, _min.y, _max.y);
            p.z = Mathf.Clamp(p.z, _min.z, _max.z);
            transform.position = p;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 c = (_min + _max) * 0.5f;
            Vector3 s = (_max - _min);
            Gizmos.DrawWireCube(c, s);
        }
    }
}
