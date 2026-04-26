using UnityEngine;
using TMPro;

namespace GameJamToolkit.TurnBased
{
    /// <summary>HUD: displays current actor + round number.</summary>
    public sealed class TurnUI : MonoBehaviour
    {
        [SerializeField] private TurnManager _manager;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "Round {0}";

        private void OnEnable() { if (_manager != null) _manager.OnRoundStarted += Refresh; }
        private void OnDisable() { if (_manager != null) _manager.OnRoundStarted -= Refresh; }

        private void Refresh(int round)
        {
            if (_label != null) _label.text = string.Format(_format, round);
        }
    }
}
