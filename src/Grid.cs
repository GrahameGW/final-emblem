using Godot;


namespace FinalEmblem.Core
{
    public class Grid
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        private readonly Tile[] tiles;

        public Grid(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            tiles = new Tile[rows * cols];

            for (int y = 0; y < cols; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    tiles[x + y * rows] = new Tile(x, y);
                }
            }

            GD.Print($"Generated a grid of {tiles.Length} tiles ({Cols} x {Rows}");
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

            return tiles[x + y * Rows];
        }

        public Tile GetNeighborTile(Tile tile, Compass direction)
        {
            return direction switch
            {
                Compass.N => GetTile(tile.Coordinates.X, tile.Coordinates.Y + 1),
                Compass.S => GetTile(tile.Coordinates.X, tile.Coordinates.Y - 1),
                Compass.E => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y),
                Compass.W => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y),
                Compass.NE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y + 1),
                Compass.NW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y + 1),
                Compass.SE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y - 1),
                Compass.SW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y - 1),
                _ => null,
            };
        }
    }
}
