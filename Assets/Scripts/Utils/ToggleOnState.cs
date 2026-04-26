using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Utils
{
    /// <summary>Toggles a GameObject based on the current GameState.</summary>
    public sealed class ToggleOnState : MonoBehaviour
    {
        [SerializeField] private GameState _activeOn = GameState.Gameplay;
        [SerializeField] private GameObject _root;

        private void Awake() { if (_root == null) _root = gameObject; }
        private void OnEnable() { EventBus.Subscribe<GameStateChangedEvent>(HandleStateChanged); }
        private void OnDisable() { EventBus.Unsubscribe<GameStateChangedEvent>(HandleStateChanged); }

        private void HandleStateChanged(GameStateChangedEvent evt)
        {
            if (_root != null) _root.SetActive(evt.Current == _activeOn);
        }
    }
}
