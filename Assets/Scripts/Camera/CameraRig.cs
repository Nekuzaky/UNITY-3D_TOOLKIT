using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Wrapper exposing the camera rig and switching the active mode.</summary>
    public sealed class CameraRig : MonoBehaviour
    {
        public enum Mode { Follow, Orbit, FirstPerson, TopDown }

        [SerializeField] private CameraFollow _follow;
        [SerializeField] private OrbitCamera _orbit;
        [SerializeField] private FirstPersonCamera _firstPerson;
        [SerializeField] private TopDownCamera _topDown;

        public void SetMode(Mode mode)
        {
            if (_follow != null) _follow.enabled = (mode == Mode.Follow);
            if (_orbit != null) _orbit.enabled = (mode == Mode.Orbit);
            if (_firstPerson != null) _firstPerson.enabled = (mode == Mode.FirstPerson);
            if (_topDown != null) _topDown.enabled = (mode == Mode.TopDown);
        }
    }
}
