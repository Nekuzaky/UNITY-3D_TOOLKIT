using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Initializes global services before the first scene loads. Place on a
    /// GameObject in the "Boot" scene.
    /// </summary>
    [DefaultExecutionOrder(-200)]
    public sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private string _firstSceneName = "MainMenu";
        [SerializeField] private bool _loadFirstScene = true;
        [SerializeField] private GameObject[] _persistentPrefabs;

        private void Awake()
        {
            Debug.Assert(_persistentPrefabs != null, "[Bootstrapper] _persistentPrefabs is null"); // R5
            Debug.Assert(!string.IsNullOrEmpty(_firstSceneName), "[Bootstrapper] _firstSceneName is empty"); // R5

            SpawnPersistentObjects();
        }

        private void Start()
        {
            if (_loadFirstScene && SceneLoader.HasInstance)
            {
                SceneLoader.Instance.LoadScene(_firstSceneName);
            }
        }

        private void SpawnPersistentObjects()
        {
            if (_persistentPrefabs == null) return;

            // R2: fixed bound = array length
            int max = _persistentPrefabs.Length;
            for (int i = 0; i < max; i++)
            {
                var prefab = _persistentPrefabs[i];
                if (prefab == null) continue;
                var go = Instantiate(prefab); // R3 exempt: initialization, not gameplay runtime
                DontDestroyOnLoad(go);
            }
        }
    }
}
