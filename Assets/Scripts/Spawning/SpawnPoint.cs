using UnityEngine;

namespace GameJamToolkit.Spawning
{
    /// <summary>Simple marker. For spawners that iterate the SpawnPoints of a scene.</summary>
    public sealed class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private string _tag;
        public string Tag => _tag;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.4f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
