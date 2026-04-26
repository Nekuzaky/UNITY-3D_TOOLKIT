using UnityEngine;
using UnityEngine.InputSystem;
using GameJamToolkit.Combat;
using GameJamToolkit.Core;

namespace GameJamToolkit.Misc
{
    /// <summary>Classic cheats (god mode, kill all). Keep disabled in production builds.</summary>
    public sealed class CheatCodes : MonoBehaviour
    {
        [SerializeField] private InputActionReference _godModeAction;
        [SerializeField] private InputActionReference _killAllAction;
        [SerializeField] private string _enemyTag = "Enemy";

        private bool _godMode;

        private void OnEnable()
        {
            if (_godModeAction != null) _godModeAction.action.Enable();
            if (_killAllAction != null) _killAllAction.action.Enable();
        }

        private void OnDisable()
        {
            if (_godModeAction != null) _godModeAction.action.Disable();
            if (_killAllAction != null) _killAllAction.action.Disable();
        }

        private void Update()
        {
            if (_godModeAction != null && _godModeAction.action.WasPressedThisFrame()) ToggleGodMode();
            if (_killAllAction != null && _killAllAction.action.WasPressedThisFrame()) KillAll();
        }

        public void ToggleGodMode()
        {
            _godMode = !_godMode;
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
            var h = player.GetComponent<HealthComponent>();
            if (h != null) h.SetMaxHealth(_godMode ? 99999f : 100f, true);
        }

        public void KillAll()
        {
            var arr = GameObject.FindGameObjectsWithTag(_enemyTag);
            int max = arr != null ? arr.Length : 0; // R2
            for (int i = 0; i < max; i++)
            {
                var h = arr[i] != null ? arr[i].GetComponent<HealthComponent>() : null;
                if (h != null) h.Kill();
            }
        }
    }
}
