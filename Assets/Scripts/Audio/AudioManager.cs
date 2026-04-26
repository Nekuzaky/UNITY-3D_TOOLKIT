using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using GameJamToolkit.Core;

namespace GameJamToolkit.Audio
{
    /// <summary>
    /// Audio singleton. Reserves a pool of AudioSource for SFX, two sources
    /// for music (crossfade). Everything goes through PlaySfx / PlayMusic.
    /// </summary>
    [DefaultExecutionOrder(-80)]
    public sealed class AudioManager : PersistentSingleton<AudioManager>
    {
        [Header("Database")]
        [SerializeField] private AudioDatabase _database;

        [Header("Mixer")]
        [SerializeField] private AudioMixerGroup _sfxGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;

        [Header("Pool")]
        [Min(2)] [SerializeField] private int _sfxPoolSize = 16;

        private readonly List<AudioSource> _sfxPool = new List<AudioSource>();
        private AudioSource _musicA;
        private AudioSource _musicB;
        private AudioSource _activeMusic;

        protected override void Awake()
        {
            base.Awake();
            BuildSfxPool();
            BuildMusicSources();
        }

        private void BuildSfxPool()
        {
            // R2: bounded by _sfxPoolSize
            for (int i = 0; i < _sfxPoolSize; i++)
            {
                var go = new GameObject($"SFX_{i}");
                go.transform.SetParent(transform);
                var src = go.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.outputAudioMixerGroup = _sfxGroup;
                _sfxPool.Add(src);
            }
        }

        private void BuildMusicSources()
        {
            _musicA = CreateMusicSource("MusicA");
            _musicB = CreateMusicSource("MusicB");
            _activeMusic = _musicA;
        }

        private AudioSource CreateMusicSource(string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            var src = go.AddComponent<AudioSource>();
            src.loop = true;
            src.playOnAwake = false;
            src.outputAudioMixerGroup = _musicGroup;
            return src;
        }

        public void PlaySfxAt(string id, Vector3 position)
        {
            var clip = _database != null ? _database.Get(id) : null;
            if (clip == null) return;
            var src = GetFreeSfxSource();
            if (src == null) return;
            src.transform.position = position;
            src.spatialBlend = clip.SpatialBlend;
            src.clip = clip.PickRandomClip();
            src.volume = clip.Volume;
            src.pitch = clip.RandomPitch();
            src.loop = clip.Loop;
            if (src.clip != null) src.Play();
        }

        public void PlaySfx(string id) { PlaySfxAt(id, transform.position); }

        public void PlayMusic(AudioClip clip, float fadeSeconds = 1f)
        {
            Debug.Assert(_activeMusic != null, "[AudioManager.PlayMusic] _activeMusic null"); // R5
            if (clip == null) return;
            var next = _activeMusic == _musicA ? _musicB : _musicA;
            next.clip = clip;
            next.volume = 0f;
            next.Play();
            StartCoroutine(CrossfadeRoutine(_activeMusic, next, fadeSeconds));
            _activeMusic = next;
        }

        private System.Collections.IEnumerator CrossfadeRoutine(AudioSource from, AudioSource to, float duration)
        {
            float t = 0f;
            duration = Mathf.Max(0.01f, duration);
            float fromStart = from != null ? from.volume : 0f;
            // R2: bounded by duration
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float k = t / duration;
                if (from != null) from.volume = Mathf.Lerp(fromStart, 0f, k);
                if (to != null) to.volume = Mathf.Lerp(0f, 1f, k);
                yield return null;
            }
            if (from != null) from.Stop();
        }

        private AudioSource GetFreeSfxSource()
        {
            int max = _sfxPool.Count;
            for (int i = 0; i < max; i++)
            {
                if (!_sfxPool[i].isPlaying) return _sfxPool[i];
            }
            return _sfxPool[0]; // override the oldest
        }
    }
}
