using UnityEngine;

namespace GameJamToolkit.Quest
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Quest/ObjectiveKill", fileName = "ObjectiveKill")]
    public sealed class ObjectiveKill : QuestObjective
    {
        public string EnemyTag = "Enemy";
    }
}
