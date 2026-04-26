using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.UI
{
    /// <summary>Listens to DamageDealtEvent and spawns a numeric popup at the hit point.</summary>
    public sealed class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private string _poolKey = "DamageNumber";

        private void OnEnable() { EventBus.Subscribe<DamageDealtEvent>(HandleDamage); }
        private void OnDisable() { EventBus.Unsubscribe<DamageDealtEvent>(HandleDamage); }

        private void HandleDamage(DamageDealtEvent evt)
        {
            if (!ObjectPool.HasInstance) return;
            Vector3 pos = evt.HitPoint != Vector3.zero ? evt.HitPoint : (evt.Target != null ? evt.Target.transform.position : Vector3.zero);
            var go = ObjectPool.Instance.Get(_poolKey, pos + Vector3.up, Quaternion.identity);
            if (go == null) return;
            var popup = go.GetComponent<DamageNumberPopup>();
            if (popup != null) popup.SetValue(evt.Amount, false);
        }
    }
}
