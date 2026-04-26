using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>Loads a <see cref="GlobalConfig"/> and applies its settings at boot.</summary>
    [DefaultExecutionOrder(-150)]
    public sealed class GlobalConfigProvider : Singleton<GlobalConfigProvider>
    {
        [SerializeField] private GlobalConfig _config;

        public GlobalConfig Config => _config;

        protected override void Awake()
        {
            base.Awake();
            ApplySettings();
        }

        private void ApplySettings()
        {
            Debug.Assert(_config != null, "[GlobalConfigProvider] _config is null"); // R5
            if (_config == null) return;

            Application.targetFrameRate = _config.TargetFrameRate;
            QualitySettings.vSyncCount = _config.VSync ? 1 : 0;
            Physics.gravity = new Vector3(0f, _config.Gravity3D, 0f);
        }
    }
}
