using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Interaction
{
    /// <summary>Lever: toggle or one-shot. UnityEvent to a target (door, platform...).</summary>
    public sealed class LeverInteractable : InteractableBase
    {
        [SerializeField] private bool _toggle = true;
        [SerializeField] private UnityEvent _onActivated;
        [SerializeField] private UnityEvent _onDeactivated;

        public bool IsOn { get; private set; }

        public override void Interact(GameObject source)
        {
            if (_toggle)
            {
                IsOn = !IsOn;
                if (IsOn) _onActivated?.Invoke();
                else _onDeactivated?.Invoke();
            }
            else
            {
                _onActivated?.Invoke();
            }
        }
    }
}
