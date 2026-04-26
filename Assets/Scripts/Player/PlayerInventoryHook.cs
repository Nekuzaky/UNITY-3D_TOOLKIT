using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Player
{
    /// <summary>Tag-marker to identify the active inventory holder.</summary>
    public sealed class PlayerInventoryHook : MonoBehaviour
    {
        public static GameObject ActivePlayer { get; private set; }

        private void OnEnable()
        {
            ActivePlayer = gameObject;
            EventBus.Publish(new PlayerSpawnedEvent { Player = gameObject, Position = transform.position });
        }

        private void OnDisable()
        {
            if (ActivePlayer == gameObject) ActivePlayer = null;
        }
    }
}
