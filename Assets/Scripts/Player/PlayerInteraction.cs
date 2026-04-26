using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Player
{
    /// <summary>
    /// Detects the closest IInteractable inside a cone and publishes
    /// <see cref="InteractionRequestedEvent"/> on press.
    /// </summary>
    public sealed class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private InputHandler _input;
        [SerializeField] private Transform _origin;
        [Min(0f)] [SerializeField] private float _radius = 1.5f;
        [Min(0f)] [SerializeField] private float _range = 2.5f;
        [SerializeField] private LayerMask _interactableMask = ~0;

        private GameObject _currentTarget;
        public GameObject CurrentTarget => _currentTarget;

        private void Awake()
        {
            if (_input == null) _input = GetComponent<InputHandler>();
            if (_origin == null) _origin = transform;
            Debug.Assert(_input != null, "[PlayerInteraction] _input is null"); // R5
            Debug.Assert(_origin != null, "[PlayerInteraction] _origin is null"); // R5
        }

        private void Update()
        {
            UpdateTarget();
            if (_input.InteractPressed && _currentTarget != null)
            {
                EventBus.Publish(new InteractionRequestedEvent { Source = gameObject, Target = _currentTarget });
            }
        }

        private void UpdateTarget()
        {
            Vector3 origin = _origin.position;
            Vector3 dir = _origin.forward;
            if (Physics.SphereCast(origin, _radius, dir, out var hit, _range, _interactableMask, QueryTriggerInteraction.Collide))
            {
                _currentTarget = hit.collider.attachedRigidbody != null ? hit.collider.attachedRigidbody.gameObject : hit.collider.gameObject;
            }
            else
            {
                _currentTarget = null;
            }
        }
    }
}
