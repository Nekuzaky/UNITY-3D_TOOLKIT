using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using GameJamToolkit.Items;

namespace GameJamToolkit.Crafting
{
    /// <summary>Workbench: queues a craft and emits result on completion.</summary>
    public sealed class CraftingStation : MonoBehaviour
    {
        [SerializeField] private Inventory _userInventory;
        [SerializeField] private UnityEvent _onCraftStarted;
        [SerializeField] private UnityEvent _onCraftCompleted;
        [SerializeField] private UnityEvent _onCraftFailed;

        public bool IsBusy { get; private set; }

        public bool TryStart(Recipe recipe)
        {
            Debug.Assert(_userInventory != null, "[CraftingStation.TryStart] _userInventory is null"); // R5
            Debug.Assert(recipe != null, "[CraftingStation.TryStart] recipe is null"); // R5
            if (IsBusy || recipe == null) return false;
            if (!CraftingSystem.CanCraft(_userInventory, recipe)) { _onCraftFailed?.Invoke(); return false; }
            StartCoroutine(CraftRoutine(recipe));
            return true;
        }

        private IEnumerator CraftRoutine(Recipe recipe)
        {
            IsBusy = true;
            _onCraftStarted?.Invoke();
            yield return new WaitForSeconds(recipe.CraftSeconds);
            CraftingSystem.TryCraft(_userInventory, recipe);
            _onCraftCompleted?.Invoke();
            IsBusy = false;
        }
    }
}
