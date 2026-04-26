using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Follows the camera or a given transform. Useful for 3D healthbars.</summary>
    public sealed class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private bool _yAxisOnly = false;
        [SerializeField] private bool _useMainCamera = true;

        private void LateUpdate()
        {
            Transform t = _target;
            if (t == null && _useMainCamera && Camera.main != null) t = Camera.main.transform;
            if (t == null) return;
            Vector3 dir = transform.position - t.position;
            if (_yAxisOnly) dir.y = 0f;
            if (dir.sqrMagnitude < 0.0001f) return;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
