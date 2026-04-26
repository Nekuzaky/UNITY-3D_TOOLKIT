using UnityEngine;
using TMPro;
using GameJamToolkit.Entities;

namespace GameJamToolkit.UI
{
    /// <summary>Displays the current wave number.</summary>
    public sealed class WaveUI : MonoBehaviour
    {
        [SerializeField] private WaveSpawner _spawner;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "Wave {0}";

        private void OnEnable()
        {
            if (_spawner != null) _spawner.OnWaveStarted += HandleWaveStarted;
        }

        private void OnDisable()
        {
            if (_spawner != null) _spawner.OnWaveStarted -= HandleWaveStarted;
        }

        private void HandleWaveStarted(int index)
        {
            if (_label == null) return;
            _label.text = string.Format(_format, index + 1);
        }
    }
}
