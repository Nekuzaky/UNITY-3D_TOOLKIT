using UnityEngine;
using GameJamToolkit.Combat;

namespace GameJamToolkit.Items
{
    /// <summary>Weapon item: points to an equippable prefab.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Items/WeaponItem", fileName = "WeaponItem")]
    public sealed class WeaponItem : ItemBase
    {
        public GameObject WeaponPrefab;
        public WeaponConfig Config;
    }
}
