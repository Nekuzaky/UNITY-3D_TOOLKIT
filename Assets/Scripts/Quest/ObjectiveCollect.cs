using UnityEngine;

namespace GameJamToolkit.Quest
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Quest/ObjectiveCollect", fileName = "ObjectiveCollect")]
    public sealed class ObjectiveCollect : QuestObjective
    {
        public string ItemId;
    }
}
