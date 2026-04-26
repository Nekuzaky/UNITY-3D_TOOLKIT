using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Audio
{
    /// <summary>Sound catalog indexed by id.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Audio/AudioDatabase", fileName = "AudioDatabase")]
    public sealed class AudioDatabase : ScriptableObject
    {
        [SerializeField] private SoundClipConfig[] _clips;

        private Dictionary<string, SoundClipConfig> _cache;

        private void OnEnable()
        {
            BuildCache();
        }

        private void BuildCache()
        {
            _cache = new Dictionary<string, SoundClipConfig>();
            if (_clips == null) return;
            int max = _clips.Length;
            for (int i = 0; i < max; i++)
            {
                var c = _clips[i];
                if (c == null || string.IsNullOrEmpty(c.Id)) continue;
                _cache[c.Id] = c;
            }
        }

        public SoundClipConfig Get(string id)
        {
            if (_cache == null) BuildCache();
            return _cache != null && _cache.TryGetValue(id, out var c) ? c : null;
        }
    }
}
