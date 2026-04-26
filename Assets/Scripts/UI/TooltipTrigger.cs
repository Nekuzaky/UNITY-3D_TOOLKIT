using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJamToolkit.UI
{
    /// <summary>Shows a tooltip on hover.</summary>
    public sealed class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] [TextArea] private string _text;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (TooltipUI.ActiveInstance != null) TooltipUI.ActiveInstance.Show(_text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (TooltipUI.ActiveInstance != null) TooltipUI.ActiveInstance.Hide();
        }

        public void SetText(string value) { _text = value; }
    }
}
