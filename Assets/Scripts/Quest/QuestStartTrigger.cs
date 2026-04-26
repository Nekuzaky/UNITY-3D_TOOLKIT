using UnityEngine;

namespace GameJamToolkit.Quest
{
    /// <summary>Starts the quest on trigger.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class QuestStartTrigger : MonoBehaviour
    {
        [SerializeField] private QuestBase _quest;
        [SerializeField] private string _filterTag = "Player";

        private bool _consumed;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (_consumed) return;
            if (!other.CompareTag(_filterTag)) return;
            if (QuestManager.HasInstance && _quest != null) QuestManager.Instance.Begin(_quest);
            _consumed = true;
        }
    }
}
