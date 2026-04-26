using UnityEngine;

namespace GameJamToolkit.TimeSystem
{
    /// <summary>Simple weather cycle (Clear / Rain / Storm). Toggles child particles / sounds.</summary>
    public sealed class WeatherSystem : MonoBehaviour
    {
        public enum Weather { Clear, Rain, Storm }

        [SerializeField] private GameObject _rainFx;
        [SerializeField] private GameObject _stormFx;
        [SerializeField] private float _changeIntervalMin = 30f;
        [SerializeField] private float _changeIntervalMax = 90f;

        public Weather Current { get; private set; }
        private float _nextChangeAt;

        private void Update()
        {
            if (Time.time < _nextChangeAt) return;
            Current = (Weather)Random.Range(0, 3);
            ApplyWeather();
            _nextChangeAt = Time.time + Random.Range(_changeIntervalMin, _changeIntervalMax);
        }

        private void ApplyWeather()
        {
            if (_rainFx != null) _rainFx.SetActive(Current == Weather.Rain);
            if (_stormFx != null) _stormFx.SetActive(Current == Weather.Storm);
        }

        public void ForceWeather(Weather w) { Current = w; ApplyWeather(); _nextChangeAt = Time.time + 60f; }
    }
}
