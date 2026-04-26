using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Audio
{
    /// <summary>Plays an SFX in reaction to an EventBus event without coupling modules.</summary>
    public sealed class SfxOnEvent : MonoBehaviour
    {
        public enum Trigger
        {
            PlayerDied,
            EnemyKilled,
            DamageDealt,
            ScoreChanged,
            CheckpointReached,
            ItemPickedUp
        }

        [SerializeField] private Trigger _trigger;
        [SerializeField] private string _sfxId;

        private void OnEnable()
        {
            switch (_trigger)
            {
                case Trigger.PlayerDied: EventBus.Subscribe<PlayerDiedEvent>(OnPlayerDied); break;
                case Trigger.EnemyKilled: EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled); break;
                case Trigger.DamageDealt: EventBus.Subscribe<DamageDealtEvent>(OnDamage); break;
                case Trigger.ScoreChanged: EventBus.Subscribe<ScoreChangedEvent>(OnScore); break;
                case Trigger.CheckpointReached: EventBus.Subscribe<CheckpointReachedEvent>(OnCheckpoint); break;
                case Trigger.ItemPickedUp: EventBus.Subscribe<ItemPickedUpEvent>(OnItem); break;
            }
        }

        private void OnDisable()
        {
            switch (_trigger)
            {
                case Trigger.PlayerDied: EventBus.Unsubscribe<PlayerDiedEvent>(OnPlayerDied); break;
                case Trigger.EnemyKilled: EventBus.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled); break;
                case Trigger.DamageDealt: EventBus.Unsubscribe<DamageDealtEvent>(OnDamage); break;
                case Trigger.ScoreChanged: EventBus.Unsubscribe<ScoreChangedEvent>(OnScore); break;
                case Trigger.CheckpointReached: EventBus.Unsubscribe<CheckpointReachedEvent>(OnCheckpoint); break;
                case Trigger.ItemPickedUp: EventBus.Unsubscribe<ItemPickedUpEvent>(OnItem); break;
            }
        }

        private void Play(Vector3 position)
        {
            if (!AudioManager.HasInstance) return;
            AudioManager.Instance.PlaySfxAt(_sfxId, position);
        }

        private void OnPlayerDied(PlayerDiedEvent evt) { Play(evt.Position); }
        private void OnEnemyKilled(EnemyKilledEvent evt) { Play(evt.Position); }
        private void OnDamage(DamageDealtEvent evt) { Play(evt.HitPoint); }
        private void OnScore(ScoreChangedEvent evt) { Play(transform.position); }
        private void OnCheckpoint(CheckpointReachedEvent evt) { Play(evt.Position); }
        private void OnItem(ItemPickedUpEvent evt) { Play(transform.position); }
    }
}
