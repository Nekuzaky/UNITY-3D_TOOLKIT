using UnityEngine;

namespace GameJamToolkit.UI
{
    /// <summary>Forces the transform to face the main camera (popup, 3D healthbar).</summary>
    public sealed class BillboardUI : MonoBehaviour
    {
        [SerializeField] private bool _yAxisOnly = false;

        private Transform _cameraTransform;

        private void LateUpdate()
        {
            if (_cameraTransform == null && Camera.main != null) _cameraTransform = Camera.main.transform;
            if (_cameraTransform == null) return;
            Vector3 dir = transform.position - _cameraTransform.position;
            if (_yAxisOnly) dir.y = 0f;
            if (dir.sqrMagnitude < 0.0001f) return;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
