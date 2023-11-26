
using System.Collections.Generic;
using Godot;

namespace FinalEmblem.Core
{
    public class Tile
    {
        public Vector2I Coordinates { get; private set; }
        public Terrain Terrain { get; private set; }
        public Vector3 WorldPosition { get; private set; }

        public Unit Unit { get; set; }
        public readonly List<Feature> Features = new();

        public Tile NeighborEast { get; set; }
        public Tile NeighborWest { get; set; }
        public Tile NeighborNorth { get; set; }
        public Tile NeighborSouth { get; set; }
        public Tile NeighborNortheast { get; set; }
        public Tile NeighborNorthwest { get; set; }
        public Tile NeighborSoutheast { get; set; }
        public Tile NeighborSouthwest { get; set; }

        public Tile(Vector2I coords, Terrain terrain)
        {
            Coordinates = coords;
            Terrain = terrain;
        }

        public Vector3 SetWorldPosition(Vector3 gridOrigin, Vector2 cellSize, bool isXZ = false, bool invertY = false)
        {
            var coords = invertY ? Coordinates * new Vector2(1f, -1f) : Coordinates;
            Vector2 pos = coords * cellSize;
            WorldPosition = isXZ ? new Vector3(pos.X, 0f, pos.Y)  : new Vector3(pos.X, pos.Y, 0f);
            WorldPosition += gridOrigin;
            return WorldPosition;
        }

        public void SetTileNeighbor(Compass direction, Tile other)
        {
            switch (direction)
            {
                case Compass.N:
                    NeighborNorth = other;
                    if (other != null) { other.NeighborSouth = this; }
                    break;
                case Compass.S:
                    NeighborSouth = other;
                    if (other == null) { other.NeighborNorth = this; }
                    break;
                case Compass.E:
                    NeighborEast = other;
                    if (other == null) { other.NeighborWest = this; }
                    break;
                case Compass.W:
                    NeighborWest = other;
                    if (other != null) { other.NeighborEast = this; }
                    break;
                case Compass.NE:
                    NeighborNortheast = other;
                    if (other != null) { other.NeighborSouthwest = this; }
                    break;
                case Compass.NW:
                    NeighborNorthwest = other;
                    if (other != null) { other.NeighborSoutheast = this; }
                    break;
                case Compass.SE:
                    NeighborSoutheast = other;
                    if (other != null) { other.NeighborNorthwest = this; }
                    break;
                case Compass.SW:
                    NeighborSouthwest = other;
                    if (other != null) { other.NeighborNortheast = this; }
                    break;
            }
        }
    }

}
