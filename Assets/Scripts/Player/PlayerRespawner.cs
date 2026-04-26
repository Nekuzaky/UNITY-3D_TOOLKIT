using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Player
{
    /// <summary>Repositions the player at the last checkpoint on death event.</summary>
    public sealed class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] private float _respawnDelay = 1.5f;
        [SerializeField] private GameObject _player;

        private float _respawnAt = -1f;

        private void OnEnable() { EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied); }
        private void OnDisable() { EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied); }

        private void HandlePlayerDied(PlayerDiedEvent evt)
        {
            _respawnAt = Time.time + _respawnDelay;
        }

        private void Update()
        {
            if (_respawnAt < 0f || Time.time < _respawnAt) return;
            _respawnAt = -1f;
            if (_player == null) return;
            Vector3 spawn = PlayerCheckpoint.LastSavedPosition;
            _player.transform.position = spawn;
            _player.SetActive(true);
        }
    }
}
