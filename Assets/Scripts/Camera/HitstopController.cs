using System.Collections;
using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Mini slowdown (temporary timeScale) on impactful events.</summary>
    public sealed class HitstopController : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _scale = 0.1f;
        [SerializeField] private float _duration = 0.06f;

        private Coroutine _routine;

        private void OnEnable()
        {
            EventBus.Subscribe<PlayerDiedEvent>(HandlePlayerDied);
            EventBus.Subscribe<EnemyKilledEvent>(HandleEnemyKilled);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<PlayerDiedEvent>(HandlePlayerDied);
            EventBus.Unsubscribe<EnemyKilledEvent>(HandleEnemyKilled);
        }

        private void HandlePlayerDied(PlayerDiedEvent evt) { Trigger(); }
        private void HandleEnemyKilled(EnemyKilledEvent evt) { Trigger(); }

        public void Trigger()
        {
            if (_routine != null) StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            float prev = Time.timeScale;
            Time.timeScale = _scale;
            yield return new WaitForSecondsRealtime(_duration);
            Time.timeScale = prev;
        }
    }
}
