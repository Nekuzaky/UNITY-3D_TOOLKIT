namespace GameJamToolkit.Items
{
    /// <summary>Inventory slot. Null item = empty slot.</summary>
    [System.Serializable]
    public class InventorySlot
    {
        public ItemBase Item;
        public int Count;

        public bool IsEmpty => Item == null || Count <= 0;

        public void Clear() { Item = null; Count = 0; }
    }
}
