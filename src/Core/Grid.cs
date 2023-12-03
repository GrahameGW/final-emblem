using Godot;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FinalEmblem.Core
{
    public class Grid
    {
        public Vector2I Size { get; private set; }
        public Vector3 WorldOrigin { get; private set; }
        public Vector2 CellSize { get; private set; }
        public bool InvertY { get; private set; }
        public bool XZOrientation { get; private set; }

        public readonly Tile[] Tiles;

        public Grid(Vector2I size, Vector3 worldOrigin, Vector2 cellSize, bool xZOrientation = false, bool invertY = false)
        {
            Size = size;
            WorldOrigin = worldOrigin;
            CellSize = cellSize;
            InvertY = invertY;
            Tiles = new Tile[size.X * size.Y];
            XZOrientation = xZOrientation;
        }

        public Tile CreateTile(Vector2I coords, Terrain terrain)
        {
            var tile = new Tile(coords, terrain);
            tile.SetWorldPosition(WorldOrigin, CellSize, XZOrientation, InvertY);
            Tiles[coords.X + coords.Y * Size.X] = tile;
            return tile;
        }

        public Tile GetTile(Vector2I coordinates)
        {
            return GetTile(coordinates.X, coordinates.Y);
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Size.X || y >= Size.Y)
            {
                return null;
            }

            return Tiles[x + y * Size.X];
        }

        public int GetTileIndex(Tile tile)
        {
            return tile.Coordinates.X + tile.Coordinates.Y * Size.X;
        }

        public Tile GetNeighborTile(Tile tile, Compass direction)
        {
            int yInc = InvertY ? -1 : 1;
            return direction switch
            {
                Compass.N => GetTile(tile.Coordinates.X, tile.Coordinates.Y + yInc),
                Compass.S => GetTile(tile.Coordinates.X, tile.Coordinates.Y - yInc),
                Compass.E => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y),
                Compass.W => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y),
                Compass.NE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y + yInc),
                Compass.NW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y + yInc),
                Compass.SE => GetTile(tile.Coordinates.X + 1, tile.Coordinates.Y - yInc),
                Compass.SW => GetTile(tile.Coordinates.X - 1, tile.Coordinates.Y - yInc),
                _ => null,
            };
        }

        public void SetAllTileNeighbors()
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                var tile = Tiles[i];
                if (tile == null) continue;

                var nw = GetTile(tile.Coordinates + new Vector2I(-1, 1));
                var n = GetTile(tile.Coordinates + new Vector2I(0, 1));
                var ne = GetTile(tile.Coordinates + new Vector2I(1, 1));
                var e = GetTile(tile.Coordinates + Vector2I.Right);

                tile.SetTileNeighbor(InvertY ? Compass.SW : Compass.NW, nw);
                tile.SetTileNeighbor(InvertY ? Compass.S : Compass.N, n);
                tile.SetTileNeighbor(InvertY ? Compass.SE : Compass.NE, ne);
                tile.SetTileNeighbor(Compass.E, e);
            }
        }
    }


}
