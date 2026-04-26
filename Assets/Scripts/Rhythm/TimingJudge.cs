using System;
using UnityEngine;

namespace GameJamToolkit.Rhythm
{
    public enum Judgement { Miss = 0, Good = 1, Great = 2, Perfect = 3 }

    /// <summary>Judges a player input vs a target time. Pure utility.</summary>
    public static class TimingJudge
    {
        public static Judgement Evaluate(double inputDspTime, double targetDspTime, double perfectWindow = 0.05, double greatWindow = 0.1, double goodWindow = 0.18)
        {
            double diff = Math.Abs(inputDspTime - targetDspTime);
            if (diff <= perfectWindow) return Judgement.Perfect;
            if (diff <= greatWindow) return Judgement.Great;
            if (diff <= goodWindow) return Judgement.Good;
            return Judgement.Miss;
        }
    }
}
