using System.Collections.Generic;
using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Entities
{
    /// <summary>
    /// Abstract Boss base: manages phases (BossPhase) triggered by thresholds
    /// of HP. Phases are prefabs / derived scripts.
    /// </summary>
    public abstract class BossBase : EnemyBase
    {
        [SerializeField] protected List<BossPhase> _phaseList = new List<BossPhase>();
        protected int _currentPhaseIndex = -1;

        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(_phaseList != null, "[BossBase] _phaseList is null"); // R5
        }

        private void OnEnable() { _health.OnHealthChanged += HandleHealthChanged; }
        private void OnDisable() { _health.OnHealthChanged -= HandleHealthChanged; }

        private void HandleHealthChanged(float current, float max)
        {
            float ratio = max > 0f ? current / max : 0f;
            int max_n = _phaseList.Count;
            // R2: bounded by phase count
            for (int i = 0; i < max_n; i++)
            {
                var p = _phaseList[i];
                if (p == null) continue;
                if (ratio <= p.HealthThreshold && i > _currentPhaseIndex)
                {
                    EnterPhase(i);
                }
            }
        }

        protected virtual void EnterPhase(int index)
        {
            if (_currentPhaseIndex >= 0 && _currentPhaseIndex < _phaseList.Count)
                _phaseList[_currentPhaseIndex]?.OnExit(this);
            _currentPhaseIndex = index;
            _phaseList[index]?.OnEnter(this);
        }
    }
}
