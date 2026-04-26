using UnityEngine;

namespace GameJamToolkit.Stealth
{
    /// <summary>Marks a hiding spot. Tracks whether it is currently occupied.</summary>
    public sealed class HidingSpot : MonoBehaviour
    {
        [SerializeField] private float _detectionMultiplier = 0.2f;
        public bool IsOccupied { get; private set; }
        public float DetectionMultiplier => _detectionMultiplier;

        public void Occupy() { IsOccupied = true; }
        public void Vacate() { IsOccupied = false; }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0f, 1f, 0.3f);
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}
