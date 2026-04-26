using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameJamToolkit.Items
{
    public sealed class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countLabel;

        public void Refresh(InventorySlot slot)
        {
            if (slot == null || slot.IsEmpty)
            {
                if (_icon != null) { _icon.enabled = false; _icon.sprite = null; }
                if (_countLabel != null) _countLabel.text = string.Empty;
                return;
            }
            if (_icon != null) { _icon.enabled = true; _icon.sprite = slot.Item.Icon; }
            if (_countLabel != null) _countLabel.text = slot.Count > 1 ? slot.Count.ToString() : string.Empty;
        }
    }
}
