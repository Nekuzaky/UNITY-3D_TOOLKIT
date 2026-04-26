using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Snaps the camera to a child pivot of the player. No rotation: see PlayerLook.</summary>
    public sealed class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _smoothing = 30f;

        public void SetPivot(Transform pivot) { _pivot = pivot; }

        private void LateUpdate()
        {
            if (_pivot == null) return;
            transform.position = Vector3.Lerp(transform.position, _pivot.position, _smoothing * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, _pivot.rotation, _smoothing * Time.deltaTime);
        }
    }
}
