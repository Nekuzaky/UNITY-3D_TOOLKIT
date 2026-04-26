using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Crafting
{
    [CreateAssetMenu(menuName = "GameJamToolkit/Crafting/RecipeDatabase", fileName = "RecipeDatabase")]
    public sealed class RecipeDatabase : ScriptableObject
    {
        [SerializeField] private Recipe[] _recipes;
        private Dictionary<string, Recipe> _cache;

        private void OnEnable() { BuildCache(); }

        private void BuildCache()
        {
            _cache = new Dictionary<string, Recipe>();
            if (_recipes == null) return;
            int max = _recipes.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var r = _recipes[i];
                if (r == null || string.IsNullOrEmpty(r.RecipeId)) continue;
                _cache[r.RecipeId] = r;
            }
        }

        public Recipe Get(string id)
        {
            if (_cache == null) BuildCache();
            return _cache != null && _cache.TryGetValue(id, out var r) ? r : null;
        }

        public Recipe[] All => _recipes;
    }
}
