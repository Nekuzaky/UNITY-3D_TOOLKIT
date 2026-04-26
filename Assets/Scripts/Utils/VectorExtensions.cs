using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class VectorExtensions
    {
        public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);
        public static Vector2 XZ(this Vector3 v) => new Vector2(v.x, v.z);
        public static Vector3 ToVector3(this Vector2 v, float y = 0f) => new Vector3(v.x, y, v.y);
        public static Vector3 Flat(this Vector3 v) => new Vector3(v.x, 0f, v.z);
        public static Vector3 ClampMagnitude(this Vector3 v, float max) => Vector3.ClampMagnitude(v, max);
    }
}
