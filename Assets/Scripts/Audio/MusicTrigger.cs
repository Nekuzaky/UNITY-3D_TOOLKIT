using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Plays a music track on trigger enter.</summary>
    [RequireComponent(typeof(Collider))]
    public sealed class MusicTrigger : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _fadeSeconds = 1f;
        [SerializeField] private bool _onceOnly = true;

        private bool _played;

        private void Reset()
        {
            var col = GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_played && _onceOnly) return;
            if (!other.CompareTag("Player")) return;
            if (AudioManager.HasInstance && _clip != null) AudioManager.Instance.PlayMusic(_clip, _fadeSeconds);
            _played = true;
        }
    }
}
