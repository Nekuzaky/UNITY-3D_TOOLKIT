using System;
using UnityEngine;

namespace GameJamToolkit.Sports
{
    /// <summary>Per-team match score. Notifies on changes.</summary>
    public sealed class MatchScore : MonoBehaviour
    {
        [SerializeField] private int[] _teamScores = new int[2];

        public int GetScore(int teamId)
        {
            Debug.Assert(_teamScores != null, "[MatchScore.GetScore] _teamScores is null"); // R5
            if (teamId < 0 || teamId >= _teamScores.Length) return 0;
            return _teamScores[teamId];
        }

        public event Action<int, int> OnScoreChanged; // teamId, newScore

        public void AddPoint(int teamId, int amount = 1)
        {
            Debug.Assert(amount > 0, "[MatchScore.AddPoint] amount <= 0"); // R5
            if (teamId < 0 || teamId >= _teamScores.Length) return;
            _teamScores[teamId] += amount;
            OnScoreChanged?.Invoke(teamId, _teamScores[teamId]);
        }

        public void Reset()
        {
            int max = _teamScores.Length; // R2
            for (int i = 0; i < max; i++) { _teamScores[i] = 0; OnScoreChanged?.Invoke(i, 0); }
        }
    }
}
