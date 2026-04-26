using System;
using UnityEngine;

namespace GameJamToolkit.Spawning
{
    /// <summary>Internal helper: fires a callback when the GameObject is disabled.</summary>
    public sealed class DespawnNotifier : MonoBehaviour
    {
        public Action OnReleased;
        private void OnDisable() { OnReleased?.Invoke(); }
    }
}
