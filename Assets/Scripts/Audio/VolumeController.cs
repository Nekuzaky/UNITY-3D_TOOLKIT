using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameJamToolkit.Audio
{
    /// <summary>Maps a Slider 0..1 to an exposed AudioMixer parameter (in dB).</summary>
    public sealed class VolumeController : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private string _exposedParam = "MasterVol";
        [SerializeField] private Slider _slider;

        private const string PrefsPrefix = "Vol_";

        private void Awake()
        {
            Debug.Assert(_mixer != null, "[VolumeController] _mixer is null"); // R5
            Debug.Assert(_slider != null, "[VolumeController] _slider is null"); // R5
            Restore();
        }

        private void OnEnable() { if (_slider != null) _slider.onValueChanged.AddListener(HandleChanged); }
        private void OnDisable() { if (_slider != null) _slider.onValueChanged.RemoveListener(HandleChanged); }

        private void HandleChanged(float value)
        {
            if (_mixer == null) return;
            float db = LinearToDecibels(value);
            _mixer.SetFloat(_exposedParam, db);
            PlayerPrefs.SetFloat(PrefsPrefix + _exposedParam, value);
        }

        private void Restore()
        {
            float saved = PlayerPrefs.GetFloat(PrefsPrefix + _exposedParam, 1f);
            if (_slider != null) _slider.value = saved;
            if (_mixer != null) _mixer.SetFloat(_exposedParam, LinearToDecibels(saved));
        }

        private static float LinearToDecibels(float linear)
        {
            return linear <= 0.0001f ? -80f : Mathf.Log10(Mathf.Clamp01(linear)) * 20f;
        }
    }
}
