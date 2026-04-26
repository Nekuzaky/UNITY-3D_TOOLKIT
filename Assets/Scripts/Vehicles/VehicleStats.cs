using UnityEngine;

namespace GameJamToolkit.Vehicles
{
    /// <summary>External vehicle config. Plug into any VehicleControllerBase derivative.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Vehicles/VehicleStats", fileName = "VehicleStats")]
    public sealed class VehicleStats : ScriptableObject
    {
        [Min(0f)] public float MaxSpeed = 30f;
        [Min(0f)] public float Acceleration = 1500f;
        [Min(0f)] public float TurnRate = 60f;
        [Min(0f)] public float BrakeForce = 2500f;
        [Min(0f)] public float Drag = 0.3f;
        [Min(0f)] public float AngularDrag = 2f;
    }
}
