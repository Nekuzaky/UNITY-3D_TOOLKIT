using System;
using UnityEngine;

namespace GameJamToolkit.Rhythm
{
    /// <summary>Accumulates rhythm score with combo and per-judgement weight.</summary>
    public sealed class RhythmScore : MonoBehaviour
    {
        [SerializeField] private int _perfectPoints = 100;
        [SerializeField] private int _greatPoints = 60;
        [SerializeField] private int _goodPoints = 25;

        public int Score { get; private set; }
        public int Combo { get; private set; }
        public int MaxCombo { get; private set; }
        public event Action<Judgement, int, int> OnRecorded;

        public void Record(Judgement j)
        {
            int delta = j switch
            {
                Judgement.Perfect => _perfectPoints,
                Judgement.Great => _greatPoints,
                Judgement.Good => _goodPoints,
                _ => 0,
            };
            if (j == Judgement.Miss) Combo = 0;
            else
            {
                Combo++;
                if (Combo > MaxCombo) MaxCombo = Combo;
            }
            Score += delta * Mathf.Max(1, Combo);
            OnRecorded?.Invoke(j, Combo, Score);
        }
    }
}
