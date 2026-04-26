using UnityEngine;

namespace GameJamToolkit.TowerDefense
{
    [CreateAssetMenu(menuName = "GameJamToolkit/TowerDefense/TowerConfig", fileName = "TowerConfig")]
    public sealed class TowerConfig : ScriptableObject
    {
        public string TowerId = "tower_default";
        public string DisplayName = "Tower";
        public Sprite Icon;
        public GameObject Prefab;
        [Min(0)] public int Cost = 25;
        [Min(0f)] public float Range = 6f;
        [Min(0f)] public float FireInterval = 0.5f;
        [Min(0f)] public float Damage = 5f;
    }
}
