using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Progression
{
    /// <summary>Player-side state of a skill tree. Tracks which nodes are unlocked.</summary>
    public sealed class SkillTree : MonoBehaviour
    {
        [SerializeField] private int _availablePoints = 5;
        private readonly HashSet<string> _unlockedSet = new HashSet<string>();

        public int AvailablePoints => _availablePoints;
        public event System.Action<SkillNode> OnUnlocked;

        public bool IsUnlocked(SkillNode node) => node != null && _unlockedSet.Contains(node.SkillId);

        public bool CanUnlock(SkillNode node)
        {
            if (node == null) return false;
            if (_unlockedSet.Contains(node.SkillId)) return false;
            if (_availablePoints < node.Cost) return false;
            if (node.Prerequisites == null) return true;
            int max = node.Prerequisites.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var p = node.Prerequisites[i];
                if (p != null && !_unlockedSet.Contains(p.SkillId)) return false;
            }
            return true;
        }

        public bool TryUnlock(SkillNode node)
        {
            if (!CanUnlock(node)) return false;
            _availablePoints -= node.Cost;
            _unlockedSet.Add(node.SkillId);
            OnUnlocked?.Invoke(node);
            return true;
        }

        public void GrantPoints(int amount)
        {
            Debug.Assert(amount >= 0, "[SkillTree.GrantPoints] amount is negative"); // R5
            _availablePoints += Mathf.Max(0, amount);
        }
    }
}
