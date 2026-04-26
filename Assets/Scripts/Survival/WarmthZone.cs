using UnityEngine;

namespace GameJamToolkit.Survival
{
    /// <summary>Restores a "warmth" Need while the player stays inside (campfire, shelter).</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class WarmthZone : MonoBehaviour
    {
        [SerializeField] private string _needId = "warmth";
        [Min(0f)] [SerializeField] private float _restorePerSecond = 5f;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerStay(Collider other)
        {
            var manager = other.GetComponentInParent<NeedsManager>();
            if (manager == null) return;
            var need = manager.Get(_needId);
            if (need == null) return;
            need.Restore(_restorePerSecond * Time.deltaTime);
        }
    }
}
