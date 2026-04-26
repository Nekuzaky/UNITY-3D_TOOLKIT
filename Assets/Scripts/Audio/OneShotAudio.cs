using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Small helper: plays a clip at a point without touching the global pool.</summary>
    public static class OneShotAudio
    {
        public static void PlayAt(AudioClip clip, Vector3 worldPos, float volume = 1f)
        {
            Debug.Assert(clip != null, "[OneShotAudio.PlayAt] clip null"); // R5
            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, worldPos, Mathf.Clamp01(volume));
        }
    }
}
