using UnityEngine;
using GameJamToolkit.UI;

namespace GameJamToolkit.Interaction
{
    public sealed class DialogueInteractable : InteractableBase
    {
        [SerializeField] [TextArea] private string[] _lines;
        [SerializeField] private DialogueBoxUI _dialogueBox;

        public override void Interact(GameObject source)
        {
            if (_dialogueBox != null && _lines != null && _lines.Length > 0) _dialogueBox.Show(_lines);
        }
    }
}
