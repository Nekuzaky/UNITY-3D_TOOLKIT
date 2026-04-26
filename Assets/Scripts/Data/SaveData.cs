using System;
using System.Collections.Generic;

namespace GameJamToolkit.Data
{
    /// <summary>Serializable container. Map of key -> serialized blob per object.</summary>
    [Serializable]
    public class SaveData
    {
        public int Version = 1;
        public long Timestamp;
        public List<SaveEntry> Entries = new List<SaveEntry>();
    }

    [Serializable]
    public class SaveEntry
    {
        public string Key;
        public string State;
    }
}
