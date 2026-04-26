using GameJamToolkit.Items;

namespace GameJamToolkit.Crafting
{
    /// <summary>An item + count required by a recipe.</summary>
    [System.Serializable]
    public class RecipeIngredient
    {
        public ItemBase Item;
        [UnityEngine.Min(1)] public int Count = 1;
    }
}
