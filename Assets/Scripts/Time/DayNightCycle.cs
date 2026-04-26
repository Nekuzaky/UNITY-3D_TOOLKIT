using UnityEngine;

namespace GameJamToolkit.TimeSystem
{
    /// <summary>
    /// Day/night cycle based on a day duration. Rotates a directional light
    /// + interpolate ambient color. Sufficient for jams.
    /// </summary>
    public sealed class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private Light _sun;
        [Min(1f)] [SerializeField] private float _dayDurationSeconds = 120f;
        [SerializeField] private Gradient _ambientGradient;
        [SerializeField] private Gradient _sunColorGradient;

        public float TimeOfDay01 { get; private set; }

        private void Update()
        {
            TimeOfDay01 = Mathf.Repeat(Time.time / Mathf.Max(1f, _dayDurationSeconds), 1f);
            float sunAngle = Mathf.Lerp(-90f, 270f, TimeOfDay01);
            if (_sun != null)
            {
                _sun.transform.rotation = Quaternion.Euler(sunAngle, 30f, 0f);
                if (_sunColorGradient != null) _sun.color = _sunColorGradient.Evaluate(TimeOfDay01);
            }
            if (_ambientGradient != null) RenderSettings.ambientLight = _ambientGradient.Evaluate(TimeOfDay01);
        }
    }
}
