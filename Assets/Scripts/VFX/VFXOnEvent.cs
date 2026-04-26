using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.VFX
{
    /// <summary>Spawns a pooled VFX in reaction to an EventBus event.</summary>
    public sealed class VFXOnEvent : MonoBehaviour
    {
        public enum Trigger { DamageDealt, EnemyKilled, PlayerDied, ItemPickedUp, CheckpointReached }

        [SerializeField] private Trigger _trigger;
        [SerializeField] private string _poolKey;

        private void OnEnable() { Subscribe(true); }
        private void OnDisable() { Subscribe(false); }

        private void Subscribe(bool on)
        {
            switch (_trigger)
            {
                case Trigger.DamageDealt: if (on) EventBus.Subscribe<DamageDealtEvent>(OnDamage); else EventBus.Unsubscribe<DamageDealtEvent>(OnDamage); break;
                case Trigger.EnemyKilled: if (on) EventBus.Subscribe<EnemyKilledEvent>(OnEnemy); else EventBus.Unsubscribe<EnemyKilledEvent>(OnEnemy); break;
                case Trigger.PlayerDied: if (on) EventBus.Subscribe<PlayerDiedEvent>(OnPlayer); else EventBus.Unsubscribe<PlayerDiedEvent>(OnPlayer); break;
                case Trigger.ItemPickedUp: if (on) EventBus.Subscribe<ItemPickedUpEvent>(OnItem); else EventBus.Unsubscribe<ItemPickedUpEvent>(OnItem); break;
                case Trigger.CheckpointReached: if (on) EventBus.Subscribe<CheckpointReachedEvent>(OnCp); else EventBus.Unsubscribe<CheckpointReachedEvent>(OnCp); break;
            }
        }

        private void Spawn(Vector3 pos) { VFXSpawner.Spawn(_poolKey, pos, Quaternion.identity); }
        private void OnDamage(DamageDealtEvent e) { Spawn(e.HitPoint); }
        private void OnEnemy(EnemyKilledEvent e) { Spawn(e.Position); }
        private void OnPlayer(PlayerDiedEvent e) { Spawn(e.Position); }
        private void OnItem(ItemPickedUpEvent e) { Spawn(transform.position); }
        private void OnCp(CheckpointReachedEvent e) { Spawn(e.Position); }
    }
}
