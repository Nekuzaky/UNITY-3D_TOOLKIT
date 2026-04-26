using System;
using UnityEngine;

namespace GameJamToolkit.TimeSystem
{
    /// <summary>Fires UnityEvents when crossing a given time of day.</summary>
    [RequireComponent(typeof(DayNightCycle))]
    public sealed class TimeOfDayEvents : MonoBehaviour
    {
        [Serializable] public class Trigger { [Range(0f, 1f)] public float At; public UnityEngine.Events.UnityEvent Event; }
        [SerializeField] private Trigger[] _triggers;

        private DayNightCycle _cycle;
        private float _previous;

        private void Awake()
        {
            _cycle = GetComponent<DayNightCycle>();
            Debug.Assert(_cycle != null, "[TimeOfDayEvents] _cycle is null"); // R5
            _previous = _cycle.TimeOfDay01;
        }

        private void Update()
        {
            if (_cycle == null || _triggers == null) return;
            float current = _cycle.TimeOfDay01;
            int max = _triggers.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var t = _triggers[i];
                if (t == null || t.Event == null) continue;
                bool crossed = (_previous < t.At && current >= t.At) || (_previous > current && (_previous < t.At || current >= t.At));
                if (crossed) t.Event.Invoke();
            }
            _previous = current;
        }
    }
}
