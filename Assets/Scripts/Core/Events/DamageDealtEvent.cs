using UnityEngine;

namespace GameJamToolkit.Core.Events
{
    public struct DamageDealtEvent
    {
        public GameObject Source;
        public GameObject Target;
        public float Amount;
        public Vector3 HitPoint;
    }
}
