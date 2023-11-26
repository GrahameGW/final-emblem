using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class GameMap : TileMap
    {
       // [Export] PackedScene unit;
        
        private Vector2 gridOffset;
        private Vector2I gridSize;
        private Rect2I gameRect;

        private Grid grid;

        // CDL = Custom Data Layer
        private const string CDL_TERRAIN = "CDL_TERRAIN";
        private const int TERRAIN_BASE_LAYER = 0;


        public Grid GenerateGridFromMap()
        {
            gameRect = GetUsedRect();
            gridOffset = gameRect.Position * TileSet.TileSize + GlobalPosition;
            var gridOrigin = new Vector3(gridOffset.X, gridOffset.Y, 0f);
            gridSize = gameRect.Size;


            grid = new Grid(gridSize, gridOrigin, TileSet.TileSize, invertY: true);
            GD.Print($"GI: {gridOffset} | GS: {gridSize} | Rect: {gameRect}");
            // tiles are ordered by x, y (e.g., (0,0), (0, 1), (0, 2) etc.)
            for (int x = 0; x < gridSize.X; x++)
            {
                for (int y = 0; y < gridSize.Y; y++)
                {
                    var coord = new Vector2I(x, y);
                    var tilemapCoords = coord + gameRect.Position;
                    var tile = GetCellTileData(TERRAIN_BASE_LAYER, tilemapCoords);
                    if (tile == null)
                    {
                        GD.Print("foo");
                    }
                    int terrainIdx = tile.GetCustomData(CDL_TERRAIN).AsInt32();
                    grid.CreateTile(coord, (Terrain)terrainIdx);
                }
            }

            return grid;
        }
        /*
        private void GenerateMapFromGrid(Grid grid)
        {
            ErrorOnMislabeledTilesets(0, CDL_TERRAIN);
            this.grid = grid;
            foreach (var tile in grid.tiles)
            {
                var atlasCoords = tile.Terrain.Index == TerrainIndex.Grass ? new Vector2I(1, 7) : new Vector2I(18, 4);
                SetCell(0, tile.Coordinates, 0, atlasCoords);
            }
        }
        */
        private void ErrorOnMislabeledTilesets(int layerIndex, string layerName)
        {
            var layer = TileSet.GetCustomDataLayerName(layerIndex);
            if (layer != layerName)
            {
                GD.PrintErr($"Tileset custom data layer {layerIndex} is not named {layerName}. " +
                    $"Ensure it is properly named in the Godot inspector then rerun the program");
            }
        }
    }
}

