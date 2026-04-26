using UnityEngine;

namespace GameJamToolkit.Stealth
{
    /// <summary>Component listening for noise emissions. Routes them to a SuspicionMeter.</summary>
    public sealed class NoiseListener : MonoBehaviour
    {
        [SerializeField] private SuspicionMeter _meter;
        [SerializeField] private float _suspicionPerNoise = 30f;

        public void OnNoise(Vector3 sourcePosition, GameObject source)
        {
            if (_meter == null) return;
            _meter.Add(_suspicionPerNoise);
        }
    }
}
