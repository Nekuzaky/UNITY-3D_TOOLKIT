using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Progression
{
    /// <summary>Listens to EnemyKilledEvent and awards XP to a target ExperienceSystem.</summary>
    public sealed class XpRewardOnKill : MonoBehaviour
    {
        [SerializeField] private ExperienceSystem _target;
        [SerializeField] private int _xpPerKill = 10;

        private void OnEnable() { EventBus.Subscribe<EnemyKilledEvent>(HandleKill); }
        private void OnDisable() { EventBus.Unsubscribe<EnemyKilledEvent>(HandleKill); }

        private void HandleKill(EnemyKilledEvent evt)
        {
            if (_target != null) _target.Add(_xpPerKill);
        }
    }
}
