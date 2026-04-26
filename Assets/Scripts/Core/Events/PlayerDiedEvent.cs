using UnityEngine;

namespace GameJamToolkit.Core.Events
{
    public struct PlayerDiedEvent
    {
        public GameObject Player;
        public Vector3 Position;
        public float Score;
    }
}
