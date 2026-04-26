using System.Collections;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Run coroutines from non-MonoBehaviour callers (ScriptableObject,
    /// static classes, services).
    /// </summary>
    public sealed class CoroutineRunner : PersistentSingleton<CoroutineRunner>
    {
        public Coroutine Run(IEnumerator routine)
        {
            Debug.Assert(routine != null, "[CoroutineRunner.Run] routine is null"); // R5
            if (routine == null) return null;
            return StartCoroutine(routine);
        }

        public void Stop(Coroutine routine)
        {
            if (routine != null) StopCoroutine(routine);
        }

        public void StopAll()
        {
            StopAllCoroutines();
        }
    }
}
