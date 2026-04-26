using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Player
{
    /// <summary>Ground checkpoint. Activates when the player walks through, stores its position.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class PlayerCheckpoint : MonoBehaviour
    {
        [SerializeField] private string _checkpointId = "cp1";
        public static Vector3 LastSavedPosition { get; private set; }
        public static string LastCheckpointId { get; private set; }

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null) return;
            if (!other.CompareTag("Player")) return;
            LastSavedPosition = transform.position;
            LastCheckpointId = _checkpointId;
            EventBus.Publish(new CheckpointReachedEvent { CheckpointId = _checkpointId, Position = transform.position });
        }
    }
}
