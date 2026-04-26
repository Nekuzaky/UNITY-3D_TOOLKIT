using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Follows a target with offset and exponential smoothing.</summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 4f, -8f);
        [SerializeField] private float _positionLerp = 8f;
        [SerializeField] private float _rotationLerp = 6f;
        [SerializeField] private bool _lookAtTarget = true;

        public void SetTarget(Transform t) { _target = t; }

        private void LateUpdate()
        {
            if (_target == null) return;
            Vector3 desired = _target.position + _target.TransformVector(_offset);
            transform.position = Vector3.Lerp(transform.position, desired, _positionLerp * Time.deltaTime);
            if (_lookAtTarget)
            {
                Quaternion goal = Quaternion.LookRotation(_target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, goal, _rotationLerp * Time.deltaTime);
            }
        }
    }
}
