using UnityEngine;

namespace GameJamToolkit.Items
{
    /// <summary>Key item. Used to unlock a door / chest / quest objective.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Items/KeyItem", fileName = "KeyItem")]
    public sealed class KeyItem : ItemBase
    {
        public string LockId;
    }
}
