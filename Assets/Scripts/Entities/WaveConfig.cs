using System;
using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>Enemy wave config. List of spawns per pool key.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Entities/WaveConfig", fileName = "WaveConfig")]
    public sealed class WaveConfig : ScriptableObject
    {
        [Serializable]
        public class WaveEntry
        {
            public string PoolKey = "Enemy";
            [Min(1)] public int Count = 5;
            [Min(0f)] public float SpawnInterval = 0.5f;
            [Min(0f)] public float StartDelay = 0f;
        }

        [Min(0f)] public float DelayBeforeWave = 1f;
        [Min(0f)] public float DelayAfterWave = 2f;
        public WaveEntry[] Entries;
    }
}
