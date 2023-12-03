
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

namespace FinalEmblem.Core
{
    [DebuggerDisplay("{Coordinates}")]
    public class Tile
    {
        public Vector2I Coordinates { get; private set; }
        public Terrain Terrain { get; private set; }
        public Vector3 WorldPosition { get; set; }

        public Unit Unit { get; set; }
        public readonly List<Feature> Features = new();

        public Tile East { get; set; }
        public Tile West { get; set; }
        public Tile North { get; set; }
        public Tile South { get; set; }
        public Tile Northeast { get; set; }
        public Tile Northwest { get; set; }
        public Tile Southeast { get; set; }
        public Tile Southwest { get; set; }

        public Tile(Vector2I coords, Terrain terrain)
        {
            Coordinates = coords;
            Terrain = terrain;
        }

        public Vector3 SetWorldPosition(Vector3 gridOrigin, Vector2 cellSize, bool isXZ = false, bool invertY = false)
        {
            Vector2 pos = Coordinates * cellSize;
            WorldPosition = isXZ ? new Vector3(pos.X, 0f, pos.Y)  : new Vector3(pos.X, pos.Y, 0f);
            WorldPosition += gridOrigin;
            return WorldPosition;
        }

        public void SetTileNeighbor(Compass direction, Tile other)
        {
            switch (direction)
            {
                case Compass.N:
                    North = other;
                    if (other != null) { other.South = this; }
                    break;
                case Compass.S:
                    South = other;
                    if (other != null) { other.North = this; }
                    break;
                case Compass.E:
                    East = other;
                    if (other != null) { other.West = this; }
                    break;
                case Compass.W:
                    West = other;
                    if (other != null) { other.East = this; }
                    break;
                case Compass.NE:
                    Northeast = other;
                    if (other != null) { other.Southwest = this; }
                    break;
                case Compass.NW:
                    Northwest = other;
                    if (other != null) { other.Southeast = this; }
                    break;
                case Compass.SE:
                    Southeast = other;
                    if (other != null) { other.Northwest = this; }
                    break;
                case Compass.SW:
                    Southwest = other;
                    if (other != null) { other.Northeast = this; }
                    break;
            }
        }

        public List<Tile> GetNeighbors(bool includeDiagonals = false)
        {
            var neighbors = includeDiagonals ? new List<Tile>
            {
                North, Northeast, East, Southeast, South, Southwest, West, Northwest
            } : new List<Tile>
            {
                North, East, South, West
            };
            return neighbors.Where(n => n != null).ToList();
        }
        public Tile[] GetNeighborsAsArray(bool includeDiagonals = false)
        {
            return includeDiagonals ? new Tile[]
            {
                North, Northeast, East, Southeast, South, Southwest, West, Northwest
            } : new Tile[]
            {
                North, East, South, West
            };
        }
    }

    public static class TileExtensions
    {
        public static int DistanceTo(this Tile tile, Tile other, bool diagonalEdges = false)
        {
            int x = Mathf.Abs(tile.Coordinates.X - other.Coordinates.X);
            int y = Mathf.Abs(tile.Coordinates.Y - other.Coordinates.Y);

            if (diagonalEdges)
            {
                int delta = Mathf.Abs(y - x);
                return x > y ? y + delta : x + delta;
            }
            else
            {
                return x + y;
            }
        }

        public static bool IsNeighborOf(this Tile tile, Tile other, bool diagonalEdges = false)
        {
            var delta = tile.Coordinates - other.Coordinates;
            if (diagonalEdges)
            {
                delta.X = Mathf.Abs(delta.X);
                delta.Y = Mathf.Abs(delta.Y);
                return (delta.X == 0 || delta.X == 1) && (delta.Y == 0 || delta.Y == 1);
            }
            else
            {
                return delta.LengthSquared() == 1;
            }
        }

        public static bool IsDiagonalTo(this Tile tile, Tile other)
        {
            var delta = tile.Coordinates - other.Coordinates;
            return Mathf.Abs(delta.X) - Mathf.Abs(delta.Y) == 0;
        }
    }

}
