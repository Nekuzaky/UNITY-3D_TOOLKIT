using UnityEngine;

namespace GameJamToolkit.LocalMultiplayer
{
    /// <summary>Per-slot score utilities. Sort + winner determination.</summary>
    public sealed class LocalScoreBoard : MonoBehaviour
    {
        [SerializeField] private LocalPlayerManager _manager;

        public LocalPlayer GetWinner()
        {
            Debug.Assert(_manager != null, "[LocalScoreBoard.GetWinner] _manager is null"); // R5
            if (_manager == null || _manager.CurrentCount == 0) return null;
            LocalPlayer best = null;
            int bestScore = int.MinValue;
            int max = _manager.Players.Count; // R2
            for (int i = 0; i < max; i++)
            {
                var p = _manager.Players[i];
                if (p == null) continue;
                if (p.Score > bestScore) { bestScore = p.Score; best = p; }
            }
            return best;
        }
    }
}
