using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>
    /// Unity-serializable wrapper to expose an IDamageable in the inspector.
    /// The inspector cannot serialize an interface; we serialize the
    /// Component reference and resolve the interface at runtime.
    /// </summary>
    public sealed class IDamageableHolder : MonoBehaviour
    {
        [SerializeField] private Component _target;

        public IDamageable Get()
        {
            if (_target is IDamageable d) return d;
            if (_target == null) return GetComponentInParent<IDamageable>();
            return _target.GetComponent<IDamageable>();
        }
    }
}
