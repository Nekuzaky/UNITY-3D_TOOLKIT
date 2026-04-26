using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Grid3D
{
    /// <summary>Sparse occupancy map: GridCoord -> GameObject. Walkable check is O(1).</summary>
    public sealed class GridOccupancy : MonoBehaviour
    {
        private readonly Dictionary<GridCoord, GameObject> _cellsDict = new Dictionary<GridCoord, GameObject>();

        public bool IsOccupied(GridCoord c) => _cellsDict.ContainsKey(c);
        public GameObject GetOccupant(GridCoord c) => _cellsDict.TryGetValue(c, out var go) ? go : null;

        public bool TryOccupy(GridCoord c, GameObject who)
        {
            Debug.Assert(who != null, "[GridOccupancy.TryOccupy] who is null"); // R5
            if (_cellsDict.ContainsKey(c)) return false;
            _cellsDict[c] = who;
            return true;
        }

        public void Free(GridCoord c) { _cellsDict.Remove(c); }
        public void FreeOf(GameObject who)
        {
            Debug.Assert(who != null, "[GridOccupancy.FreeOf] who is null"); // R5
            GridCoord? toRemove = null;
            foreach (var pair in _cellsDict)
            {
                if (pair.Value == who) { toRemove = pair.Key; break; }
            }
            if (toRemove.HasValue) _cellsDict.Remove(toRemove.Value);
        }
    }
}
