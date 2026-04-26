using System;
using System.Collections.Generic;
using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Quest
{
    /// <summary>
    /// Tracks active quests. Reacts to EventBus events
    /// to progress objectives (KillEnemy, ItemPickedUp, ZoneReach).
    /// </summary>
    public sealed class QuestManager : PersistentSingleton<QuestManager>
    {
        private readonly List<QuestBase> _activeQuests = new List<QuestBase>();
        public event Action<QuestBase> OnQuestStarted;
        public event Action<QuestBase> OnQuestCompleted;
        public event Action<QuestBase, QuestObjective> OnObjectiveProgressed;

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyKilledEvent>(HandleEnemyKilled);
            EventBus.Subscribe<ItemPickedUpEvent>(HandleItemPicked);
            EventBus.Subscribe<CheckpointReachedEvent>(HandleZoneReached);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyKilledEvent>(HandleEnemyKilled);
            EventBus.Unsubscribe<ItemPickedUpEvent>(HandleItemPicked);
            EventBus.Unsubscribe<CheckpointReachedEvent>(HandleZoneReached);
        }

        public void Begin(QuestBase quest)
        {
            Debug.Assert(quest != null, "[QuestManager.Begin] quest is null"); // R5
            if (quest == null || _activeQuests.Contains(quest)) return;
            // R2: bounded by length objectives
            int max = quest.Objectives != null ? quest.Objectives.Length : 0;
            for (int i = 0; i < max; i++) { if (quest.Objectives[i] != null) quest.Objectives[i].Reset(); }
            quest.State = QuestState.Active;
            _activeQuests.Add(quest);
            OnQuestStarted?.Invoke(quest);
        }

        public void Abandon(QuestBase quest)
        {
            if (quest == null) return;
            quest.State = QuestState.Failed;
            _activeQuests.Remove(quest);
        }

        private void Progress(QuestBase quest, QuestObjective obj, int delta)
        {
            obj.AddProgress(delta);
            OnObjectiveProgressed?.Invoke(quest, obj);
            if (quest.AllObjectivesComplete())
            {
                quest.State = QuestState.Completed;
                if (ScoreManager.HasInstance && quest.ScoreReward > 0) ScoreManager.Instance.Add(quest.ScoreReward);
                OnQuestCompleted?.Invoke(quest);
                _activeQuests.Remove(quest);
            }
        }

        private void HandleEnemyKilled(EnemyKilledEvent evt)
        {
            int max = _activeQuests.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var q = _activeQuests[i];
                if (q == null || q.Objectives == null) continue;
                int oMax = q.Objectives.Length;
                for (int j = 0; j < oMax; j++)
                {
                    if (q.Objectives[j] is ObjectiveKill ok)
                    {
                        if (evt.Enemy != null && evt.Enemy.CompareTag(ok.EnemyTag)) Progress(q, ok, 1);
                    }
                }
            }
        }

        private void HandleItemPicked(ItemPickedUpEvent evt)
        {
            int max = _activeQuests.Count;
            for (int i = 0; i < max; i++)
            {
                var q = _activeQuests[i];
                if (q == null || q.Objectives == null) continue;
                int oMax = q.Objectives.Length;
                for (int j = 0; j < oMax; j++)
                {
                    if (q.Objectives[j] is ObjectiveCollect oc && oc.ItemId == evt.ItemId) Progress(q, oc, evt.Amount);
                }
            }
        }

        private void HandleZoneReached(CheckpointReachedEvent evt)
        {
            int max = _activeQuests.Count;
            for (int i = 0; i < max; i++)
            {
                var q = _activeQuests[i];
                if (q == null || q.Objectives == null) continue;
                int oMax = q.Objectives.Length;
                for (int j = 0; j < oMax; j++)
                {
                    if (q.Objectives[j] is ObjectiveReach orh && orh.ZoneId == evt.CheckpointId) Progress(q, orh, 1);
                }
            }
        }

        public IReadOnlyList<QuestBase> Active => _activeQuests;
    }
}
