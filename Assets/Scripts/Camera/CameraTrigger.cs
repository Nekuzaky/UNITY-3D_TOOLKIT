using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Camera switch on trigger: turns off the old one, turns on the new one.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class CameraTrigger : MonoBehaviour
    {
        [SerializeField] private Camera _enableOnEnter;
        [SerializeField] private Camera _disableOnEnter;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_disableOnEnter != null) _disableOnEnter.enabled = false;
            if (_enableOnEnter != null) _enableOnEnter.enabled = true;
        }
    }
}
