using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.TurnBased
{
    /// <summary>
    /// Drives a turn-based game loop. Sorts actors by initiative each round
    /// and ticks BeginTurn/EndTurn one at a time.
    /// </summary>
    public sealed class TurnManager : MonoBehaviour
    {
        private readonly List<ITurnActor> _actorList = new List<ITurnActor>();
        public int CurrentTurnIndex { get; private set; }
        public int RoundNumber { get; private set; }
        public ITurnActor CurrentActor { get; private set; }

        public event System.Action<ITurnActor> OnTurnStarted;
        public event System.Action<ITurnActor> OnTurnEnded;
        public event System.Action<int> OnRoundStarted;

        public void Register(ITurnActor actor)
        {
            Debug.Assert(actor != null, "[TurnManager.Register] actor is null"); // R5
            if (actor == null || _actorList.Contains(actor)) return;
            _actorList.Add(actor);
        }

        public void Unregister(ITurnActor actor)
        {
            if (actor == null) return;
            _actorList.Remove(actor);
        }

        public void StartCombat()
        {
            Debug.Assert(_actorList.Count > 0, "[TurnManager.StartCombat] no actors"); // R5
            RoundNumber = 0;
            BeginRound();
        }

        private void BeginRound()
        {
            RoundNumber++;
            _actorList.Sort((a, b) => b.Initiative.CompareTo(a.Initiative));
            CurrentTurnIndex = 0;
            OnRoundStarted?.Invoke(RoundNumber);
            BeginCurrentTurn();
        }

        private void BeginCurrentTurn()
        {
            // R2: bounded by actor count
            int safety = _actorList.Count + 1;
            while (safety-- > 0 && CurrentTurnIndex < _actorList.Count && !_actorList[CurrentTurnIndex].IsAlive)
            {
                CurrentTurnIndex++;
            }
            if (CurrentTurnIndex >= _actorList.Count) { BeginRound(); return; }
            CurrentActor = _actorList[CurrentTurnIndex];
            CurrentActor.BeginTurn();
            OnTurnStarted?.Invoke(CurrentActor);
        }

        private void Update()
        {
            if (CurrentActor == null || !CurrentActor.IsTurnComplete) return;
            EndCurrentTurn();
        }

        public void EndCurrentTurn()
        {
            if (CurrentActor == null) return;
            CurrentActor.EndTurn();
            OnTurnEnded?.Invoke(CurrentActor);
            CurrentTurnIndex++;
            if (CurrentTurnIndex >= _actorList.Count) BeginRound();
            else BeginCurrentTurn();
        }
    }
}
