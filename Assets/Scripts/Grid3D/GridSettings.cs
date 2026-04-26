using UnityEngine;

namespace GameJamToolkit.Grid3D
{
    /// <summary>Grid configuration: cell size + origin. Use a SO so multiple grids can share rules.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/Grid3D/GridSettings", fileName = "GridSettings")]
    public sealed class GridSettings : ScriptableObject
    {
        [Min(0.01f)] public float CellSize = 1f;
        public float YPlane = 0f;

        public Vector3 ToWorld(GridCoord c) => new Vector3(c.X * CellSize, YPlane, c.Z * CellSize);

        public GridCoord ToCoord(Vector3 worldPos)
        {
            int x = Mathf.RoundToInt(worldPos.x / CellSize);
            int z = Mathf.RoundToInt(worldPos.z / CellSize);
            return new GridCoord(x, z);
        }
    }
}
