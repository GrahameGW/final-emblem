using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class GameMap : TileMap
    {
       // [Export] PackedScene unit;
        
        private Vector2 gridWorldOrigin;
        private Rect2I gameRect;
        private Grid grid;

        // CDL = Custom Data Layer
        private const string CDL_TERRAIN = "CDL_TERRAIN";
        private const int TERRAIN_BASE_LAYER = 0;


        public override void _Ready()
        {
            GenerateGridFromMap();
        }

        public Grid GenerateGridFromMap()
        {
            gameRect = GetUsedRect();
            gridWorldOrigin = ToGlobal(MapToLocal(gameRect.Position));

            var vec3gridOrigin = new Vector3(gridWorldOrigin.X, gridWorldOrigin.Y, 0f);
            grid = new Grid(gameRect.Size, vec3gridOrigin, TileSet.TileSize, invertY: true);
            GD.Print($"GI: {gridWorldOrigin} | GS: {gameRect.Size} | Rect: {gameRect}");

            // tiles are ordered by x, y (e.g., (0,0), (0, 1), (0, 2) etc.)
            // y gets bigger as it goes down
            for (int x = 0; x < grid.Size.X; x++)
            {
                for (int y = 0; y < grid.Size.Y; y++)
                {
                    var coord = new Vector2I(x, y);
                    var tilemapCoords = coord + gameRect.Position;
                    var tile = GetCellTileData(TERRAIN_BASE_LAYER, tilemapCoords);
                    int terrainIdx = tile.GetCustomData(CDL_TERRAIN).AsInt32();
                    grid.CreateTile(coord, (Terrain)terrainIdx);
                }
            }

            grid.SetAllTileNeighbors();
            return grid;
        }

        public override void _Input(InputEvent input)
        {
            if (input is InputEventMouseButton && input.IsPressed())
            {
                var pos = GetGlobalMousePosition();
                var tilePos = LocalToMap(ToLocal(pos));
                var tile = grid.GetTile(tilePos - gameRect.Position);
                GD.Print($"TM Coords: {tilePos} Tile: {tile?.Coordinates} Terrain: {tile?.Terrain}");
            }
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

