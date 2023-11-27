using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.Core
{
    public static class NavService
    {
        private static Grid grid;

        public static void SetGridInstance(Grid instance)
        {
            grid = instance;
        }

        public static List<Tile> FindTilesInRange(int maxDistance, Tile start, bool includeStart = true, bool diagonalEdges = false)
        {
            var tiles = grid.Tiles.Select(t => new SearchItem { tile = t }).ToArray();
            var frontier = new Queue<SearchItem>();
            var inRange = new List<Tile>();

            var t0 = tiles[grid.GetTileIndex(start)];
            t0.distance = 0;
            frontier.Enqueue(t0);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                inRange.Add(current.tile);
                if (current.distance == maxDistance)
                {
                    continue;
                }
                var neighbors = current.tile.GetNeighbors(diagonalEdges);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    int index = grid.GetTileIndex(neighbors[i]);
                    var next = tiles[index];
                    if (next.distance != int.MaxValue)
                    {
                        continue;
                    }
                    next.distance = current.distance + 1;
                    frontier.Enqueue(next);
                }
            }

            if (!includeStart)
            {
                inRange.RemoveAt(0);
            }
            return inRange;
        }

        private class SearchItem
        {
            public Tile tile;
            public int distance = int.MaxValue;
        }
    }

}
