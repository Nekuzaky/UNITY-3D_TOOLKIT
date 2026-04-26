using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Data
{
    /// <summary>
    /// JSON save / load. ISaveable objects register themselves.
    /// File in Application.persistentDataPath/save_{slot}.json.
    /// </summary>
    public sealed class SaveManager : PersistentSingleton<SaveManager>
    {
        private readonly Dictionary<string, ISaveable> _saveableDict = new Dictionary<string, ISaveable>();

        public void Register(ISaveable saveable)
        {
            Debug.Assert(saveable != null, "[SaveManager.Register] saveable is null"); // R5
            if (saveable == null || string.IsNullOrEmpty(saveable.SaveKey)) return;
            _saveableDict[saveable.SaveKey] = saveable;
        }

        public void Unregister(ISaveable saveable)
        {
            if (saveable == null) return;
            _saveableDict.Remove(saveable.SaveKey);
        }

        public bool Save(int slot)
        {
            Debug.Assert(slot >= 0, "[SaveManager.Save] slot is negative"); // R5
            var data = new SaveData { Timestamp = System.DateTime.UtcNow.Ticks };
            foreach (var pair in _saveableDict)
            {
                if (pair.Value == null) continue;
                data.Entries.Add(new SaveEntry { Key = pair.Key, State = pair.Value.SerializeState() });
            }
            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(GetPath(slot), json);
                return true;
            }
            catch (System.Exception e) { Debug.LogError($"[SaveManager] write error: {e.Message}"); return false; }
        }

        public bool Load(int slot)
        {
            Debug.Assert(slot >= 0, "[SaveManager.Load] slot is negative"); // R5
            string path = GetPath(slot);
            if (!File.Exists(path)) return false;
            try
            {
                string json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<SaveData>(json);
                if (data == null || data.Entries == null) return false;
                int max = data.Entries.Count;
                for (int i = 0; i < max; i++)
                {
                    var entry = data.Entries[i];
                    if (entry == null) continue;
                    if (_saveableDict.TryGetValue(entry.Key, out var s)) s.DeserializeState(entry.State);
                }
                return true;
            }
            catch (System.Exception e) { Debug.LogError($"[SaveManager] read error: {e.Message}"); return false; }
        }

        public bool DeleteSlot(int slot)
        {
            string path = GetPath(slot);
            if (!File.Exists(path)) return false;
            try { File.Delete(path); return true; }
            catch { return false; }
        }

        public bool SlotExists(int slot) => File.Exists(GetPath(slot));

        private static string GetPath(int slot) => Path.Combine(Application.persistentDataPath, $"save_{slot}.json");
    }
}
