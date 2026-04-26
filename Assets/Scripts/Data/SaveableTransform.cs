using System;
using UnityEngine;

namespace GameJamToolkit.Data
{
    /// <summary>Saves a transform position / rotation / scale.</summary>
    public sealed class SaveableTransform : MonoBehaviour, ISaveable
    {
        [SerializeField] private string _saveKey = "transform";
        public string SaveKey => _saveKey;

        [Serializable]
        private class State
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public Vector3 Scale;
        }

        private void OnEnable() { if (SaveManager.HasInstance) SaveManager.Instance.Register(this); }
        private void OnDisable() { if (SaveManager.HasInstance) SaveManager.Instance.Unregister(this); }

        public string SerializeState()
        {
            var s = new State { Position = transform.position, Rotation = transform.rotation, Scale = transform.localScale };
            return JsonUtility.ToJson(s);
        }

        public void DeserializeState(string state)
        {
            if (string.IsNullOrEmpty(state)) return;
            var s = JsonUtility.FromJson<State>(state);
            if (s == null) return;
            transform.SetPositionAndRotation(s.Position, s.Rotation);
            transform.localScale = s.Scale;
        }
    }
}
