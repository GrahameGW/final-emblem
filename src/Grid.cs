using Godot;

namespace FinalEmblem.Core
{
    public class Grid
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public readonly Tile[] tiles;
        public Vector2 WorldOffset { get; private set; }
        public Vector2 CellSize { get; private set; }

        public Grid(int cols, int rows, Vector2 cellSize, Vector2 offset)
        {
            Rows = rows;
            Cols = cols;
            tiles = new Tile[rows * cols];
            CellSize = cellSize;
            WorldOffset = offset;

            GD.Print($"Generated a grid of {tiles.Length} tiles ({Cols} x {Rows})");
        }

        public Tile GetTile(Vector2I coordinates)
        {
            return GetTile(coordinates.X, coordinates.Y);
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Cols || y >= Rows)
            {
                return null;
            }

            return tiles[x * Rows + y];
        }

        public Tile GetNeighborTile(Tile tile, Compass direction)
        {
            return direction switch
            {
                Compass.N => GetTile(tile.Coordinates.X, tile.Coordinates.Y - 1),
                Compass.S => GetTile(tile.Coordinates.X, tile.Coordinates.Y + 1),
                Compass.E => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y),
                Compass.W => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y),
                Compass.NE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y - 1),
                Compass.NW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y - 1),
                Compass.SE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y + 1),
                Compass.SW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y + 1),
                _ => null,
            };
        }

        public Vector2 GetTilePosition(Tile tile)
        {
            var xy = new Vector2(tile.Coordinates.X * CellSize.X, tile.Coordinates.Y * CellSize.Y);
            return xy + WorldOffset;
        }

        public void CreateTile(int index, Vector2I coordinates, TileTerrain terrain)
        {
            if (index < 0 || index >= tiles.Length)
            {
                throw new System.ArgumentOutOfRangeException(
                    $"Index not available in grid array (provided {index}, needs value between 0 and {tiles.Length - 1}"
                    );
            }

            tiles[index] = new Tile(coordinates, terrain, this);
        }
    }
}
