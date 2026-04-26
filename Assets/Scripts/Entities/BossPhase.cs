using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>A boss phase. Subclass for scripted attacks / patterns.</summary>
    public abstract class BossPhase : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _healthThreshold = 0.5f;
        public float HealthThreshold => _healthThreshold;

        public virtual void OnEnter(BossBase boss) { }
        public virtual void OnExit(BossBase boss) { }
    }
}
