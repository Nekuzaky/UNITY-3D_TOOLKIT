using System;
using UnityEngine;
using GameJamToolkit.Core;

namespace GameJamToolkit.Data
{
    /// <summary>Owns the active table. Notifies listeners on language switch.</summary>
    public sealed class LocalizationManager : PersistentSingleton<LocalizationManager>
    {
        [SerializeField] private LocalizationTable[] _tableArray;
        [SerializeField] private string _defaultLanguage = "fr";

        public LocalizationTable ActiveTable { get; private set; }
        public event Action OnLanguageChanged;

        protected override void Awake()
        {
            base.Awake();
            SetLanguage(_defaultLanguage);
        }

        public void SetLanguage(string code)
        {
            Debug.Assert(_tableArray != null, "[LocalizationManager.SetLanguage] _tableArray is null"); // R5
            if (_tableArray == null) return;
            int max = _tableArray.Length;
            for (int i = 0; i < max; i++)
            {
                if (_tableArray[i] != null && _tableArray[i].LanguageCode == code)
                {
                    ActiveTable = _tableArray[i];
                    OnLanguageChanged?.Invoke();
                    return;
                }
            }
        }

        public string Get(string key) => ActiveTable != null ? ActiveTable.Get(key) : key;
    }
}
