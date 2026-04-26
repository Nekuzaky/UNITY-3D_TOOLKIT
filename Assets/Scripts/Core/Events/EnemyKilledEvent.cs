using UnityEngine;

namespace GameJamToolkit.Core.Events
{
    public struct EnemyKilledEvent
    {
        public GameObject Enemy;
        public Vector3 Position;
        public int ScoreReward;
    }
}
