using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.LocalMultiplayer
{
    /// <summary>
    /// Spawns a player prefab when an input device joins. Wraps Unity's
    /// PlayerInputManager but with our naming/conventions and slot tracking.
    /// </summary>
    public sealed class LocalPlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [Min(1)] [SerializeField] private int _maxPlayers = 4;

        private readonly List<LocalPlayer> _players = new List<LocalPlayer>();
        public IReadOnlyList<LocalPlayer> Players => _players;
        public int CurrentCount => _players.Count;

        public LocalPlayer Spawn(InputDevice device)
        {
            Debug.Assert(_playerPrefab != null, "[LocalPlayerManager.Spawn] _playerPrefab is null"); // R5
            if (CurrentCount >= _maxPlayers) return null;
            int slot = CurrentCount;
            Vector3 pos = _spawnPoints != null && _spawnPoints.Length > slot && _spawnPoints[slot] != null
                ? _spawnPoints[slot].position : transform.position;
            var go = Instantiate(_playerPrefab, pos, Quaternion.identity); // R3 exempt: scene init spawn
            var lp = go.GetComponent<LocalPlayer>();
            if (lp == null) lp = go.AddComponent<LocalPlayer>();
            lp.SetSlot(slot);
            _players.Add(lp);
            return lp;
        }
    }
}
