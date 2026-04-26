using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Randomizes pitch + volume of an AudioSource on each external PlayOneShot.</summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class PitchedRandomizer : MonoBehaviour
    {
        [SerializeField] private Vector2 _pitchRange = new Vector2(0.95f, 1.05f);
        [SerializeField] private Vector2 _volumeRange = new Vector2(0.9f, 1f);

        private AudioSource _source;

        private void Awake() { _source = GetComponent<AudioSource>(); }

        public void PlayClip(AudioClip clip)
        {
            Debug.Assert(_source != null, "[PitchedRandomizer.PlayClip] _source is null"); // R5
            Debug.Assert(clip != null, "[PitchedRandomizer.PlayClip] clip null"); // R5
            if (_source == null || clip == null) return;
            _source.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
            _source.PlayOneShot(clip, Random.Range(_volumeRange.x, _volumeRange.y));
        }
    }
}
