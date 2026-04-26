using UnityEngine;

namespace GameJamToolkit.Grid3D
{
    /// <summary>Integer cell coordinate (x, z). Y is the height layer.</summary>
    [System.Serializable]
    public struct GridCoord
    {
        public int X;
        public int Z;

        public GridCoord(int x, int z) { X = x; Z = z; }

        public static GridCoord Zero => new GridCoord(0, 0);

        public override bool Equals(object obj) => obj is GridCoord o && o.X == X && o.Z == Z;
        public override int GetHashCode() => X * 73856093 ^ Z * 19349663;
        public override string ToString() => $"({X},{Z})";

        public static GridCoord operator +(GridCoord a, GridCoord b) => new GridCoord(a.X + b.X, a.Z + b.Z);
        public static GridCoord operator -(GridCoord a, GridCoord b) => new GridCoord(a.X - b.X, a.Z - b.Z);
        public static bool operator ==(GridCoord a, GridCoord b) => a.X == b.X && a.Z == b.Z;
        public static bool operator !=(GridCoord a, GridCoord b) => !(a == b);

        public int ManhattanDistance(GridCoord other) => Mathf.Abs(X - other.X) + Mathf.Abs(Z - other.Z);
    }
}
