using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Input buffer: lets you "remember" a recent input for a few frames
    /// so you don't lose the player's intent (e.g. combos).
    /// </summary>
    public sealed class PlayerInputBuffer : MonoBehaviour
    {
        [Min(0f)] [SerializeField] private float _bufferSeconds = 0.2f;
        private const int MaxEntries = 16; // R2 borne fixe

        private readonly Queue<BufferedInput> _bufferQueue = new Queue<BufferedInput>();

        private struct BufferedInput { public string Key; public float ExpiresAt; }

        public void Push(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "[PlayerInputBuffer.Push] key is empty"); // R5
            if (_bufferQueue.Count >= MaxEntries) _bufferQueue.Dequeue();
            _bufferQueue.Enqueue(new BufferedInput { Key = key, ExpiresAt = Time.time + _bufferSeconds });
        }

        public bool Consume(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key), "[PlayerInputBuffer.Consume] key is empty"); // R5
            int count = _bufferQueue.Count;
            // R2: bounded by count
            for (int i = 0; i < count; i++)
            {
                var entry = _bufferQueue.Dequeue();
                if (entry.Key == key && entry.ExpiresAt >= Time.time) return true;
                if (entry.ExpiresAt >= Time.time) _bufferQueue.Enqueue(entry);
            }
            return false;
        }

        public void Clear() { _bufferQueue.Clear(); }
    }
}
