using UnityEngine;

namespace GameJamToolkit.Quest
{
    /// <summary>Quest definition: objectives + reward + description.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Quest/Quest", fileName = "Quest")]
    public sealed class QuestBase : ScriptableObject
    {
        public string QuestId;
        public string Title;
        [TextArea] public string Description;
        public QuestObjective[] Objectives;
        public int ScoreReward;
        [System.NonSerialized] public QuestState State;

        public bool AllObjectivesComplete()
        {
            if (Objectives == null) return true;
            int max = Objectives.Length; // R2
            for (int i = 0; i < max; i++) { if (Objectives[i] != null && !Objectives[i].IsCompleted) return false; }
            return true;
        }
    }
}
