using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Top-down camera (RTS / dungeon crawler).</summary>
    public sealed class TopDownCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _height = 12f;
        [SerializeField] private float _distance = 4f;
        [SerializeField] private float _angle = 60f;
        [SerializeField] private float _smoothing = 8f;

        private void LateUpdate()
        {
            if (_target == null) return;
            Quaternion rot = Quaternion.Euler(_angle, 0f, 0f);
            Vector3 desired = _target.position + new Vector3(0f, _height, -_distance);
            transform.position = Vector3.Lerp(transform.position, desired, _smoothing * Time.deltaTime);
            transform.rotation = rot;
        }

        public void SetTarget(Transform t) { _target = t; }
    }
}
