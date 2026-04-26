using UnityEngine;
using TMPro;

namespace GameJamToolkit.Data
{
    /// <summary>Binds a TMP_Text to a localization key. Refreshes on switch.</summary>
    public sealed class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _label;

        private void Awake()
        {
            if (_label == null) _label = GetComponent<TMP_Text>();
            Debug.Assert(_label != null, "[LocalizedText] _label is null"); // R5
        }
        private void OnEnable()
        {
            Refresh();
            if (LocalizationManager.HasInstance) LocalizationManager.Instance.OnLanguageChanged += Refresh;
        }
        private void OnDisable()
        {
            if (LocalizationManager.HasInstance) LocalizationManager.Instance.OnLanguageChanged -= Refresh;
        }

        public void SetKey(string key) { _key = key; Refresh(); }

        private void Refresh()
        {
            if (_label == null) return;
            _label.text = LocalizationManager.HasInstance ? LocalizationManager.Instance.Get(_key) : $"<{_key}>";
        }
    }
}
