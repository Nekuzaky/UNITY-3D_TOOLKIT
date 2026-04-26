using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Grid3D
{
    /// <summary>
    /// Simple BFS pathfinder on a 4-neighborhood grid with occupancy. Returns
    /// a list of coords from start (exclusive) to goal (inclusive).
    /// Bounded by maxNodes to keep within R2.
    /// </summary>
    public sealed class GridPathfinder : MonoBehaviour
    {
        [SerializeField] private GridOccupancy _occupancy;
        [SerializeField] private int _maxNodes = 4096;

        private static readonly GridCoord[] Directions = new[]
        {
            new GridCoord(1, 0), new GridCoord(-1, 0),
            new GridCoord(0, 1), new GridCoord(0, -1)
        };

        public bool TryFindPath(GridCoord start, GridCoord goal, List<GridCoord> resultList)
        {
            Debug.Assert(resultList != null, "[GridPathfinder.TryFindPath] resultList is null"); // R5
            Debug.Assert(_maxNodes > 0, "[GridPathfinder.TryFindPath] _maxNodes <= 0"); // R5
            resultList.Clear();
            if (start == goal) return true;

            var visited = new Dictionary<GridCoord, GridCoord>();
            var queue = new Queue<GridCoord>();
            queue.Enqueue(start);
            visited[start] = start;

            int explored = 0;
            // R2: bounded by _maxNodes
            while (queue.Count > 0 && explored < _maxNodes)
            {
                explored++;
                var cur = queue.Dequeue();
                if (cur == goal) return Reconstruct(start, goal, visited, resultList);

                for (int i = 0; i < 4; i++)
                {
                    var next = cur + Directions[i];
                    if (visited.ContainsKey(next)) continue;
                    if (_occupancy != null && _occupancy.IsOccupied(next) && next != goal) continue;
                    visited[next] = cur;
                    queue.Enqueue(next);
                }
            }
            return false;
        }

        private static bool Reconstruct(GridCoord start, GridCoord goal, Dictionary<GridCoord, GridCoord> from, List<GridCoord> output)
        {
            var cur = goal;
            int safety = from.Count + 1; // R2
            while (cur != start && safety-- > 0)
            {
                output.Add(cur);
                if (!from.TryGetValue(cur, out cur)) return false;
            }
            output.Reverse();
            return true;
        }
    }
}
