using UnityEngine;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Core
{
    /// <summary>Global score. Notifies listeners through <see cref="ScoreChangedEvent"/>.</summary>
    public sealed class ScoreManager : PersistentSingleton<ScoreManager>
    {
        [SerializeField] private int _initialScore = 0;
        [SerializeField] private int _maxScore = 999_999_999; // R2 upper cap

        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CurrentScore = _initialScore;
            HighScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        public void Add(int amount)
        {
            Debug.Assert(amount >= 0, "[ScoreManager.Add] amount is negative"); // R5
            if (amount <= 0) return;
            int previous = CurrentScore;
            CurrentScore = Mathf.Min(_maxScore, CurrentScore + amount);
            EventBus.Publish(new ScoreChangedEvent { Previous = previous, Current = CurrentScore, Delta = CurrentScore - previous });
            UpdateHighScore();
        }

        public void Reset()
        {
            int previous = CurrentScore;
            CurrentScore = _initialScore;
            EventBus.Publish(new ScoreChangedEvent { Previous = previous, Current = CurrentScore, Delta = CurrentScore - previous });
        }

        private void UpdateHighScore()
        {
            if (CurrentScore <= HighScore) return;
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }
    }
}
