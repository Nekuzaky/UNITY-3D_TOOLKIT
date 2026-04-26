using UnityEngine;
using GameJamToolkit.Items;

namespace GameJamToolkit.Crafting
{
    /// <summary>Crafting recipe SO: list of ingredients + result + craft time.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Crafting/Recipe", fileName = "Recipe")]
    public sealed class Recipe : ScriptableObject
    {
        public string RecipeId = "recipe_default";
        public string DisplayName = "Recipe";
        public Sprite Icon;
        public RecipeIngredient[] Ingredients;
        public ItemBase Result;
        [Min(1)] public int ResultCount = 1;
        [Min(0f)] public float CraftSeconds = 1f;
    }
}
