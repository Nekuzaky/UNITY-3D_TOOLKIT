using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.Interaction
{
    public sealed class ButtonInteractable : InteractableBase
    {
        [SerializeField] private UnityEvent _onPressed;

        public override void Interact(GameObject source) { _onPressed?.Invoke(); }
    }
}
