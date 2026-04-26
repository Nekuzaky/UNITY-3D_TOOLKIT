using UnityEngine;

namespace GameJamToolkit.Interaction
{
    /// <summary>Implemented by any interactable object. PlayerInteraction invokes it.</summary>
    public interface IInteractable
    {
        bool CanInteract(GameObject source);
        void Interact(GameObject source);
        string Prompt { get; }
    }
}
