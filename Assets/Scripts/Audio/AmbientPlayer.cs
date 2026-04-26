using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Plays a low-volume looping ambient sample.</summary>
    public sealed class AmbientPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [Range(0f, 1f)] [SerializeField] private float _volume = 0.4f;

        private AudioSource _source;

        private void Awake()
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.loop = true;
            _source.playOnAwake = false;
            _source.spatialBlend = 0f;
        }

        private void OnEnable()
        {
            if (_clip == null) return;
            _source.clip = _clip;
            _source.volume = _volume;
            _source.Play();
        }

        private void OnDisable()
        {
            if (_source != null) _source.Stop();
        }
    }
}
