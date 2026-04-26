using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Updates the follow camera target on PlayerSpawnedEvent.</summary>
    public sealed class CameraTargetSwitcher : MonoBehaviour
    {
        [SerializeField] private CameraFollow _follow;
        [SerializeField] private OrbitCamera _orbit;
        [SerializeField] private TopDownCamera _topDown;

        private void OnEnable() { EventBus.Subscribe<PlayerSpawnedEvent>(HandleSpawn); }
        private void OnDisable() { EventBus.Unsubscribe<PlayerSpawnedEvent>(HandleSpawn); }

        private void HandleSpawn(PlayerSpawnedEvent evt)
        {
            if (evt.Player == null) return;
            if (_follow != null) _follow.SetTarget(evt.Player.transform);
            if (_orbit != null) _orbit.SetTarget(evt.Player.transform);
            if (_topDown != null) _topDown.SetTarget(evt.Player.transform);
        }
    }
}
