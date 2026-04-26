using UnityEngine;
using GameJamToolkit.Core;
using GameJamToolkit.Core.Events;

namespace GameJamToolkit.Interaction
{
    /// <summary>Abstract base. Listens to InteractionRequestedEvent to avoid coupling with PlayerInteraction.</summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string _prompt = "Interact";
        [SerializeField] private bool _oneShot = false;

        protected bool _consumed;

        public string Prompt => _prompt;

        protected virtual void OnEnable() { EventBus.Subscribe<InteractionRequestedEvent>(HandleInteractionRequested); }
        protected virtual void OnDisable() { EventBus.Unsubscribe<InteractionRequestedEvent>(HandleInteractionRequested); }

        private void HandleInteractionRequested(InteractionRequestedEvent evt)
        {
            if (evt.Target != gameObject) return;
            if (!CanInteract(evt.Source)) return;
            Interact(evt.Source);
            if (_oneShot) _consumed = true;
        }

        public virtual bool CanInteract(GameObject source) { return !_consumed; }
        public abstract void Interact(GameObject source);
    }
}
