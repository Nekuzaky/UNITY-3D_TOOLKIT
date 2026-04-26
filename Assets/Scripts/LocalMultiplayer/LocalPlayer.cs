using UnityEngine;

namespace GameJamToolkit.LocalMultiplayer
{
    /// <summary>Marker for a local player slot. Stores the slot index + score.</summary>
    public sealed class LocalPlayer : MonoBehaviour
    {
        [SerializeField] private int _slot;
        public int Slot => _slot;
        public int Score { get; private set; }

        public void SetSlot(int slot) { _slot = slot; }
        public void AddScore(int amount) { Score += Mathf.Max(0, amount); }
        public void ResetScore() { Score = 0; }
    }
}
