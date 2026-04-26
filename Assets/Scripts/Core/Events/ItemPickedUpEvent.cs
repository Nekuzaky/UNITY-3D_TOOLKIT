using UnityEngine;

namespace GameJamToolkit.Core.Events
{
    public struct ItemPickedUpEvent
    {
        public GameObject Picker;
        public string ItemId;
        public int Amount;
    }
}
