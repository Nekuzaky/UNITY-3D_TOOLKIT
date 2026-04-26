using System;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>Pool configuration: prefab + initial size + key.</summary>
    [Serializable]
    public class PoolEntry
    {
        public string Key;
        public GameObject Prefab;
        [Min(1)] public int InitialSize = 8;
        [Min(1)] public int MaxSize = 256;
    }
}
