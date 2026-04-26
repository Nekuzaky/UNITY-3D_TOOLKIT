using UnityEngine;

namespace GameJamToolkit.Sports
{
    /// <summary>Bridges a Goal trigger -> MatchScore + ball reset.</summary>
    public sealed class GoalToScore : MonoBehaviour
    {
        [SerializeField] private Goal _goal;
        [SerializeField] private MatchScore _score;
        [SerializeField] private BallController _ball;

        private void Awake()
        {
            Debug.Assert(_goal != null, "[GoalToScore] _goal is null"); // R5
            Debug.Assert(_score != null, "[GoalToScore] _score is null"); // R5
        }

        private void OnEnable()
        {
            if (_goal == null) return;
            // We can't subscribe directly to UnityEvent<int> here; the Goal exposes
            // it via inspector wiring. Project can either wire the inspector or
            // call HandleGoal() from a button.
        }

        public void HandleGoal(int teamId)
        {
            if (_score != null) _score.AddPoint(teamId);
            if (_ball != null) _ball.ResetToSpawn();
        }
    }
}
