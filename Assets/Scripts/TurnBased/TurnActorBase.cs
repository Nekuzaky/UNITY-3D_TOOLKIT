using UnityEngine;

namespace GameJamToolkit.TurnBased
{
    /// <summary>
    /// Convenient base for ITurnActor implementations. Combines ActionPoints
    /// reset with begin/end turn hooks.
    /// </summary>
    [RequireComponent(typeof(ActionPoints))]
    public abstract class TurnActorBase : MonoBehaviour, ITurnActor
    {
        [SerializeField] private int _initiative = 10;
        protected ActionPoints _points;

        public int Initiative => _initiative;
        public abstract bool IsAlive { get; }
        public bool IsTurnComplete { get; protected set; }

        protected virtual void Awake()
        {
            _points = GetComponent<ActionPoints>();
            Debug.Assert(_points != null, "[TurnActorBase] _points is null"); // R5
        }

        public virtual void BeginTurn()
        {
            IsTurnComplete = false;
            _points.RestoreAll();
        }

        public virtual void EndTurn()
        {
            IsTurnComplete = true;
        }
    }
}
