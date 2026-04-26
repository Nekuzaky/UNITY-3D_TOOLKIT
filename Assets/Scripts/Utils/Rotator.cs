using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Uniform rotation. For orbs, visual pickups.</summary>
    public sealed class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _eulerPerSecond = new Vector3(0f, 90f, 0f);
        [SerializeField] private bool _localSpace = true;

        private void Update()
        {
            if (_localSpace) transform.Rotate(_eulerPerSecond * Time.deltaTime, Space.Self);
            else transform.Rotate(_eulerPerSecond * Time.deltaTime, Space.World);
        }
    }
}
