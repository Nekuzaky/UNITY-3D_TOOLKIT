using UnityEngine;
using TMPro;

namespace GameJamToolkit.Quest
{
    /// <summary>Active-quest UI tracker. Basic list title + progress.</summary>
    public sealed class QuestUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        private void OnEnable()
        {
            if (QuestManager.HasInstance)
            {
                QuestManager.Instance.OnObjectiveProgressed += Refresh;
                QuestManager.Instance.OnQuestStarted += Refresh;
                QuestManager.Instance.OnQuestCompleted += Refresh;
            }
        }

        private void OnDisable()
        {
            if (QuestManager.HasInstance)
            {
                QuestManager.Instance.OnObjectiveProgressed -= Refresh;
                QuestManager.Instance.OnQuestStarted -= Refresh;
                QuestManager.Instance.OnQuestCompleted -= Refresh;
            }
        }

        private void Refresh(QuestBase _quest) { Refresh(); }
        private void Refresh(QuestBase _quest, QuestObjective _obj) { Refresh(); }

        private void Refresh()
        {
            if (_label == null || !QuestManager.HasInstance) return;
            var sb = new System.Text.StringBuilder();
            var list = QuestManager.Instance.Active;
            int max = list.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var q = list[i];
                if (q == null) continue;
                sb.AppendLine(q.Title);
                int oMax = q.Objectives != null ? q.Objectives.Length : 0;
                for (int j = 0; j < oMax; j++)
                {
                    var o = q.Objectives[j];
                    if (o == null) continue;
                    sb.AppendLine($"  - {o.Description} ({o.Current}/{o.Required})");
                }
            }
            _label.text = sb.ToString();
        }
    }
}
