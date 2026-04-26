using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.PhysicsTools
{
    /// <summary>Zone that kills instantly (infinite fall, deep water).</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class KillVolume : MonoBehaviour
    {
        [SerializeField] private LayerMask _targetMask = ~0;

        private void Reset() { var col = GetComponent<Collider>(); if (col != null) col.isTrigger = true; }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _targetMask) == 0) return;
            var h = other.GetComponentInParent<HealthComponent>();
            if (h != null) h.Kill();
        }
    }
}
