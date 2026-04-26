using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Misc
{
    /// <summary>Trigger that fires victory when the player walks through.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class WinTrigger : MonoBehaviour
    {
        [SerializeField] private string _filterTag = "Player";
        private bool _consumed;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (_consumed || !other.CompareTag(_filterTag)) return;
            _consumed = true;
            if (GameManager.HasInstance) GameManager.Instance.TriggerWin();
        }
    }
}
