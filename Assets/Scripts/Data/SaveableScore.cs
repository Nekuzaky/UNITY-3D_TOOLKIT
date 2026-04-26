using System;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Data
{
    /// <summary>Saves the current score.</summary>
    public sealed class SaveableScore : MonoBehaviour, ISaveable
    {
        [SerializeField] private string _saveKey = "score";
        public string SaveKey => _saveKey;

        [Serializable] private class State { public int Current; public int High; }

        private void OnEnable() { if (SaveManager.HasInstance) SaveManager.Instance.Register(this); }
        private void OnDisable() { if (SaveManager.HasInstance) SaveManager.Instance.Unregister(this); }

        public string SerializeState()
        {
            int cur = ScoreManager.HasInstance ? ScoreManager.Instance.CurrentScore : 0;
            int hi = ScoreManager.HasInstance ? ScoreManager.Instance.HighScore : 0;
            return JsonUtility.ToJson(new State { Current = cur, High = hi });
        }

        public void DeserializeState(string state)
        {
            if (string.IsNullOrEmpty(state) || !ScoreManager.HasInstance) return;
            var s = JsonUtility.FromJson<State>(state);
            if (s == null) return;
            ScoreManager.Instance.Reset();
            ScoreManager.Instance.Add(s.Current);
        }
    }
}
