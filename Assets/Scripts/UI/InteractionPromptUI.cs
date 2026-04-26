using UnityEngine;
using TMPro;
using GameJamToolkit.Player;

namespace GameJamToolkit.UI
{
    /// <summary>Shows "Press E" when the player is near an Interactable.</summary>
    public sealed class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _format = "Press {0}";
        [SerializeField] private string _bindingDisplay = "E";
        [SerializeField] private PlayerInteraction _interaction;

        private void Update()
        {
            if (_interaction == null || _root == null) return;
            bool show = _interaction.CurrentTarget != null;
            _root.SetActive(show);
            if (show && _label != null) _label.text = string.Format(_format, _bindingDisplay);
        }
    }
}
