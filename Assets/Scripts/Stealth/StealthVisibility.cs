using UnityEngine;

namespace GameJamToolkit.Stealth
{
    /// <summary>
    /// Tracks how visible the player is (light, hiding, motion). Reads
    /// 0..1; AI uses it to scale FieldOfView gain.
    /// </summary>
    public sealed class StealthVisibility : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _baseVisibility = 0.6f;
        [SerializeField] private HidingSpot _currentHidingSpot;

        public float Visibility { get; private set; }

        private void Update()
        {
            float v = _baseVisibility;
            if (_currentHidingSpot != null && _currentHidingSpot.IsOccupied) v *= _currentHidingSpot.DetectionMultiplier;
            Visibility = Mathf.Clamp01(v);
        }

        public void EnterHidingSpot(HidingSpot spot)
        {
            if (_currentHidingSpot != null) _currentHidingSpot.Vacate();
            _currentHidingSpot = spot;
            if (spot != null) spot.Occupy();
        }

        public void ExitHidingSpot()
        {
            if (_currentHidingSpot != null) _currentHidingSpot.Vacate();
            _currentHidingSpot = null;
        }
    }
}
