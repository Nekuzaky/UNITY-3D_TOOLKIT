using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Items
{
    /// <summary>Consumable item: applies a one-shot effect (heal, temporary buff).</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Items/Consumable", fileName = "ConsumableItem")]
    public sealed class ConsumableItem : ItemBase
    {
        public float HealAmount;
        public float StaminaRestore;

        public void Use(GameObject user)
        {
            if (user == null) return;
            if (HealAmount > 0f)
            {
                var healable = user.GetComponentInParent<IHealable>();
                if (healable != null) healable.Heal(HealAmount);
            }
            if (StaminaRestore > 0f)
            {
                var stamina = user.GetComponent<Player.PlayerStamina>();
                if (stamina != null) stamina.Restore(StaminaRestore);
            }
        }
    }
}
