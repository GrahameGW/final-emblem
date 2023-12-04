using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.Core
{
    public static class NavService
    {
        private static Grid grid;
        private static readonly Terrain[] impassable =
        {
            Terrain.Cliff,
            Terrain.Water
        };

        public static void SetGridInstance(Grid instance)
        {
            grid = instance;
        }

        public static List<Tile> FindTilesInRange(int maxDistance, Tile start, bool includeStart = true, bool diagonalEdges = false)
        {
            var tiles = grid.Tiles.Select(t => new SearchItem { Tile = t }).ToArray();
            var frontier = new SearchItemPriorityQueue();
            var inRange = new List<Tile>();

            var t0 = tiles[grid.GetTileIndex(start)];
            t0.Distance = 0;
            frontier.Enqueue(t0);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                if (impassable.Contains(current.Tile.Terrain)) { continue; }

                inRange.Add(current.Tile);
                if (current.Distance == maxDistance) { continue; }

                var neighbors = current.Tile.GetNeighbors(diagonalEdges);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    int index = grid.GetTileIndex(neighbors[i]);
                    var next = tiles[index];
                    int distance = current.Distance;
                    // add increments for distance here
                    // e.g. mud --> distance += 10;
                    distance += 1;
                    if (next.Distance == int.MaxValue)
                    {
                        next.Distance = distance;
                        frontier.Enqueue(next);
                    }
                    else if (distance < next.Distance)
                    {
                        next.Distance = distance;
                    }
                }
            }

            if (!includeStart)
            {
                inRange.RemoveAt(0);
            }
            return inRange;
        }

        public static List<Tile> FindShortestPath(Tile start, Tile end, List<Tile> availableTiles, bool includeStart = true, bool diagonalEdges = false)
        {
            SearchItem[] items = availableTiles.Select(t => new SearchItem { Tile = t }).ToArray();
            var frontier = new SearchItemPriorityQueue();
            var path = new List<Tile>();

            var t0 = items.FirstOrDefault(t => t.Tile == start);
            if (t0 == null)
            {
                frontier.Enqueue(new SearchItem
                {
                    Tile = start,
                    Distance = 0
                });
            } else
            {
                t0.Distance = 0;
                frontier.Enqueue(t0);
            }

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                // found path, log and return
                if (current.Tile == end)
                {
                    path.Add(end);
                    current = current.PathFrom;
                    while (current.Tile != start)
                    {
                        path.Add(current.Tile);
                        current = current.PathFrom;
                    }
                    break;
                }

                if (impassable.Contains(current.Tile.Terrain)) { continue; }

                var neighbors = current.Tile.GetNeighbors(diagonalEdges);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var next = items.FirstOrDefault(s => s.Tile == neighbors[i]);
                    if (next == null) { continue; }
                    int distance = current.Distance;
                    // add increments for distance here
                    // e.g. mud --> distance += 10;
                    distance += 1;
                    if (next.Distance == int.MaxValue)
                    {
                        next.Distance = distance;
                        next.PathFrom = current;
                        next.Heuristic = next.Tile.DistanceTo(current.Tile);
                        frontier.Enqueue(next);
                    }
                    else if (distance < next.Distance)
                    {
                        int oldPriority = next.Priority;
                        next.Distance = distance;
                        next.PathFrom = current;
                        frontier.Change(next, oldPriority);
                    }
                }
            }
            path.Reverse();
            return path;
        }       
    }
}
