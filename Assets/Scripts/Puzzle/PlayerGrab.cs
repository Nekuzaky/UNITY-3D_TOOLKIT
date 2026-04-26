using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJamToolkit.Puzzle
{
    /// <summary>
    /// Lets a player grab nearby Grabbable objects. Hold to keep, press again
    /// to release. Throws on release with the configured throw force.
    /// </summary>
    public sealed class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private Transform _carryAnchor;
        [SerializeField] private InputActionReference _grabAction;
        [SerializeField] private float _grabRadius = 1.5f;
        [SerializeField] private float _throwForce = 8f;
        [SerializeField] private LayerMask _grabbableMask = ~0;

        private Grabbable _current;

        private void Awake()
        {
            Debug.Assert(_carryAnchor != null, "[PlayerGrab] _carryAnchor is null"); // R5
            Debug.Assert(_grabAction != null, "[PlayerGrab] _grabAction is null"); // R5
        }

        private void OnEnable() { if (_grabAction != null) _grabAction.action.Enable(); }
        private void OnDisable() { if (_grabAction != null) _grabAction.action.Disable(); }

        private void Update()
        {
            if (_grabAction == null) return;
            if (!_grabAction.action.WasPressedThisFrame()) return;
            if (_current != null) Release();
            else TryGrabClosest();
        }

        private void TryGrabClosest()
        {
            var hits = Physics.OverlapSphere(transform.position, _grabRadius, _grabbableMask);
            int max = hits.Length; // R2
            float best = float.PositiveInfinity;
            Grabbable target = null;
            for (int i = 0; i < max; i++)
            {
                var g = hits[i].GetComponentInParent<Grabbable>();
                if (g == null || g.IsGrabbed) continue;
                float d = (g.transform.position - transform.position).sqrMagnitude;
                if (d < best) { best = d; target = g; }
            }
            if (target != null) { target.Grab(_carryAnchor); _current = target; }
        }

        private void Release()
        {
            if (_current == null) return;
            _current.Release(transform.forward * _throwForce);
            _current = null;
        }
    }
}
