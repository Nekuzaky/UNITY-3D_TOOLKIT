using UnityEngine;

namespace GameJamToolkit.Items
{
    /// <summary>Item definition. Pure data. Behavior lives in ItemPickup and derived components.</summary>
    public abstract class ItemBase : ScriptableObject
    {
        [Header("Identity")]
        public string ItemId = "item_default";
        public string DisplayName = "Item";
        [TextArea] public string Description;
        public Sprite Icon;
        public ItemRarity Rarity = ItemRarity.Common;
        public bool Stackable = false;
        [Min(1)] public int MaxStack = 1;
    }
}
