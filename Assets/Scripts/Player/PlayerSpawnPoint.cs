using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Player
{
    /// <summary>Spawns the Player prefab at its position when the scene starts.</summary>
    public sealed class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private bool _spawnOnAwake = true;

        public static PlayerSpawnPoint Active { get; private set; }
        public static GameObject SpawnedPlayer { get; private set; }

        private void Awake()
        {
            Active = this;
            if (_spawnOnAwake) Spawn();
        }

        public GameObject Spawn()
        {
            Debug.Assert(_playerPrefab != null, "[PlayerSpawnPoint.Spawn] _playerPrefab is null"); // R5
            if (_playerPrefab == null) return null;
            if (SpawnedPlayer != null) return SpawnedPlayer;

            // R3 exempt: single spawn at scene start, not in gameplay loop
            SpawnedPlayer = Instantiate(_playerPrefab, transform.position, transform.rotation);
            EventBus.Publish(new PlayerSpawnedEvent { Player = SpawnedPlayer, Position = transform.position });
            return SpawnedPlayer;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
