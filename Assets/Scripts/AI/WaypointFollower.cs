using UnityEngine;

namespace GameJamToolkit.AI
{
    /// <summary>Follows a WaypointPath. For patrol enemies, moving platforms.</summary>
    public sealed class WaypointFollower : MonoBehaviour
    {
        [SerializeField] private WaypointPath _path;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _arriveDistance = 0.1f;

        private int _currentIndex;

        private void Update()
        {
            if (_path == null || _path.Count == 0) return;
            Vector3 target = _path.GetPoint(_currentIndex);
            Vector3 to = target - transform.position;
            if (to.sqrMagnitude <= _arriveDistance * _arriveDistance)
            {
                _currentIndex = _path.Next(_currentIndex);
                return;
            }
            transform.position += to.normalized * _speed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(to.normalized), 6f * Time.deltaTime);
        }
    }
}
