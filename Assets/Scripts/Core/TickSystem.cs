using System;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Centralized tick system. Lets scripts subscribe to a global tick instead
    /// of declaring 1000 Update() methods. Improves perf when there are many
    /// objects (e.g. projectiles, custom particles).
    /// </summary>
    [DefaultExecutionOrder(-50)]
    public sealed class TickSystem : Singleton<TickSystem>
    {
        public event Action<float> OnTick;
        public event Action<float> OnFixedTick;
        public event Action<float> OnLateTick;

        private void Update()
        {
            OnTick?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            OnFixedTick?.Invoke(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            OnLateTick?.Invoke(Time.deltaTime);
        }
    }
}
