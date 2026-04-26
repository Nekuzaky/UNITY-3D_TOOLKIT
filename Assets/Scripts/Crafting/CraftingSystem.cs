using UnityEngine;
using GameJamToolkit.Items;

namespace GameJamToolkit.Crafting
{
    /// <summary>
    /// Static helpers: check if an Inventory has the ingredients of a recipe,
    /// and consume them + add the result.
    /// </summary>
    public static class CraftingSystem
    {
        public static bool CanCraft(Inventory inv, Recipe recipe)
        {
            Debug.Assert(inv != null, "[CraftingSystem.CanCraft] inv is null"); // R5
            Debug.Assert(recipe != null, "[CraftingSystem.CanCraft] recipe is null"); // R5
            if (inv == null || recipe == null || recipe.Ingredients == null) return false;
            int max = recipe.Ingredients.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var ing = recipe.Ingredients[i];
                if (ing == null || ing.Item == null) continue;
                if (!inv.Has(ing.Item, ing.Count)) return false;
            }
            return true;
        }

        public static bool TryCraft(Inventory inv, Recipe recipe)
        {
            if (!CanCraft(inv, recipe)) return false;
            int max = recipe.Ingredients.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var ing = recipe.Ingredients[i];
                if (ing == null || ing.Item == null) continue;
                ConsumeFromInventory(inv, ing.Item, ing.Count);
            }
            return inv.TryAdd(recipe.Result, recipe.ResultCount);
        }

        private static void ConsumeFromInventory(Inventory inv, ItemBase item, int amount)
        {
            int remaining = amount;
            int max = inv.Capacity; // R2
            for (int i = 0; i < max && remaining > 0; i++)
            {
                var slot = inv.GetSlot(i);
                if (slot == null || slot.Item != item) continue;
                int take = Mathf.Min(remaining, slot.Count);
                inv.TryRemove(i, take);
                remaining -= take;
            }
        }
    }
}
