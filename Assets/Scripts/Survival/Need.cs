using System;
using UnityEngine;

namespace GameJamToolkit.Survival
{
    /// <summary>
    /// Generic "need" stat (hunger, thirst, sanity, warmth). Decays over time;
    /// when empty, raises OnDepleted (which can deal damage in derived components).
    /// </summary>
    public sealed class Need : MonoBehaviour
    {
        [SerializeField] private string _id = "need";
        [Min(0f)] [SerializeField] private float _max = 100f;
        [Min(0f)] [SerializeField] private float _decayPerSecond = 0.5f;
        [Min(0f)] [SerializeField] private float _depletedTickInterval = 1f;

        public string Id => _id;
        public float Current { get; private set; }
        public float Max => _max;
        public float Ratio => _max > 0f ? Current / _max : 0f;
        public bool IsDepleted => Current <= 0f;

        public event Action<float, float> OnChanged;
        public event Action OnDepletedTick;

        private float _nextDepletedTick;

        private void Awake() { Current = _max; }

        public void Restore(float amount)
        {
            Debug.Assert(amount >= 0f, "[Need.Restore] amount is negative"); // R5
            float prev = Current;
            Current = Mathf.Min(_max, Current + amount);
            if (!Mathf.Approximately(prev, Current)) OnChanged?.Invoke(Current, _max);
        }

        public void Drain(float amount)
        {
            Debug.Assert(amount >= 0f, "[Need.Drain] amount is negative"); // R5
            float prev = Current;
            Current = Mathf.Max(0f, Current - amount);
            if (!Mathf.Approximately(prev, Current)) OnChanged?.Invoke(Current, _max);
        }

        private void Update()
        {
            if (_decayPerSecond > 0f) Drain(_decayPerSecond * Time.deltaTime);
            if (IsDepleted && Time.time >= _nextDepletedTick)
            {
                _nextDepletedTick = Time.time + _depletedTickInterval;
                OnDepletedTick?.Invoke();
            }
        }
    }
}
