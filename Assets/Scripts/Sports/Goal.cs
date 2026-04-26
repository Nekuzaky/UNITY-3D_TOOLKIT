using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Sports
{
    /// <summary>Trigger that scores when the ball enters. Identifies the team that scored.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class Goal : MonoBehaviour
    {
        [SerializeField] private int _scoringTeamId;
        [SerializeField] private string _ballTag = "Ball";
        [SerializeField] private UnityEvent<int> _onGoal;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_ballTag)) return;
            _onGoal?.Invoke(_scoringTeamId);
        }
    }
}
