using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Sound definition: id + alternate clips + pitch / volume variations.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Audio/SoundClip", fileName = "SoundClip")]
    public sealed class SoundClipConfig : ScriptableObject
    {
        public string Id = "sfx_default";
        public AudioClip[] Clips;
        [Range(0f, 1f)] public float Volume = 1f;
        public Vector2 PitchRange = new Vector2(0.95f, 1.05f);
        [Range(0f, 1f)] public float SpatialBlend = 0f;
        public bool Loop = false;

        public AudioClip PickRandomClip()
        {
            if (Clips == null || Clips.Length == 0) return null;
            return Clips[Random.Range(0, Clips.Length)];
        }

        public float RandomPitch() { return Random.Range(PitchRange.x, PitchRange.y); }
    }
}
