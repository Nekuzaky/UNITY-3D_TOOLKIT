using System;
using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Audio
{
    /// <summary>Plays a dedicated music track per GameState.</summary>
    public sealed class PlayMusicOnStateChange : MonoBehaviour
    {
        [Serializable] public class Entry { public GameState State; public AudioClip Clip; public float FadeSeconds = 1f; }
        [SerializeField] private Entry[] _entryArray;

        private void OnEnable() { EventBus.Subscribe<GameStateChangedEvent>(HandleStateChanged); }
        private void OnDisable() { EventBus.Unsubscribe<GameStateChangedEvent>(HandleStateChanged); }

        private void HandleStateChanged(GameStateChangedEvent evt)
        {
            if (_entryArray == null || !AudioManager.HasInstance) return;
            int max = _entryArray.Length;
            for (int i = 0; i < max; i++)
            {
                var entry = _entryArray[i];
                if (entry == null || entry.Clip == null) continue;
                if (entry.State == evt.Current)
                {
                    AudioManager.Instance.PlayMusic(entry.Clip, entry.FadeSeconds);
                    return;
                }
            }
        }
    }
}
