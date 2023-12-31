using Godot;

using System.Collections.Generic;
using System;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class GameMap : TileMap
    {
        private Vector2 gridWorldOrigin;
        private Rect2I gameRect;
        public Grid grid;
        private Tile selected;
        private Tile underPointer;

        public event Action<Tile> OnTileUnderMouseChanged;
        public event Action<Tile> OnSelectedTileChanged;

        // CDL = Custom Data Layer
        private const string CDL_TERRAIN = "CDL_TERRAIN";
        private const string CDL_FEATURE = "CDL_FEATURE";
        private const int TERRAIN_BASE_LAYER = 1;
        private const int TERRAIN_BASE_SOURCE = 1;
        private const int FEATURE_BASE_LAYER = 2;
        private const int FEATURE_BASE_SOURCE = 1;
        private const int NAV_OVERLAY_LAYER = 3;
        private const int NAV_OVERLAY_SOURCE = 4;

        // Debug
        public Vector2I DebugUnderPointerCoords
        {
            get => underPointer == null ? new Vector2I() : underPointer.Coordinates;
        }
        public Vector2 DebugUnderPointerWorldPos 
        {
            get => underPointer == null ? new Vector2() : underPointer.WorldPosition.Vector2XY();
        }

        public override void _UnhandledInput(InputEvent input)
        {
            if (grid == null) { return; }
            
            var mouse = GetGlobalMousePosition();
            var tile = GetGridTile(mouse);
            
            if (tile != underPointer) 
            {
                underPointer = tile;
                //GD.Print($"Under pointer: {underPointer?.Coordinates}");
                OnTileUnderMouseChanged?.Invoke(tile);
            }
            if (Input.IsActionPressed(InputAction.SUBMIT))
            {
                SelectTile(tile);
            }
        }

        public Grid GenerateGridFromMap()
        {
            gameRect = GetUsedRect();
            gridWorldOrigin = ToGlobal(MapToLocal(gameRect.Position));

            var vec3gridOrigin = new Vector3(gridWorldOrigin.X, gridWorldOrigin.Y, 0f);
            grid = new Grid(gameRect.Size, vec3gridOrigin, TileSet.TileSize, invertY: true);
            GD.Print($"GI: {gridWorldOrigin} | GS: {gameRect.Size} | Rect: {gameRect}");

            // tiles are ordered by x, y (e.g., (0,0), (0, 1), (0, 2) etc.)
            // y gets bigger as it goes down V>
            for (int x = 0; x < grid.Size.X; x++)
            {
                for (int y = 0; y < grid.Size.Y; y++)
                {
                    var coord = new Vector2I(x, y);
                    var tilemapCoords = coord + gameRect.Position;
                    var tileData = GetCellTileData(TERRAIN_BASE_LAYER, tilemapCoords);
                    
                    if (tileData == null) { continue; }

                    int terrainIdx = tileData.GetCustomData(CDL_TERRAIN).AsInt32();
                    var tile = grid.CreateTile(coord, (Terrain)terrainIdx);
                    tileData = GetCellTileData(FEATURE_BASE_LAYER, tilemapCoords);
                    if (tileData == null) { continue; };
                    
                    var resource = tileData.GetCustomData(CDL_FEATURE).As<Resource>();
                    var feature = resource as Feature;
                    tile.Feature = feature;
                }
            }

            grid.SetAllTileNeighbors();
            return grid;
        }

        public void SetUnitPositionsFromTokens(List<Unit> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                var tile = GetGridTile(tokens[i].GlobalPosition);

                tokens[i].Tile = tile;
                tokens[i].GlobalPosition = new Vector2(tile.WorldPosition.X, tile.WorldPosition.Y);
            }
        }

        private void GenerateMapFromGrid(Grid grid)
        {
            ErrorOnMislabeledTilesets(0, CDL_TERRAIN);
            this.grid = grid;
            foreach (var tile in grid.Tiles)
            {
                var atlasCoords = tile.Terrain == Terrain.Grass ? new Vector2I(1, 7) : new Vector2I(18, 4);
                SetCell(0, tile.Coordinates, 0, atlasCoords);
            }
        }

        public Tile GetGridTile(Vector2 globalPos)
        {
            var tilePos = LocalToMap(ToLocal(globalPos));
            return grid.GetTile(tilePos - gameRect.Position);
        }

        public void SelectTile(Tile tile)
        {
            selected = tile;
            GD.Print($"Selected: {selected?.Coordinates}. Unit: {selected?.Unit}. Tile.WorldPos: {selected?.WorldPosition}");
            OnSelectedTileChanged?.Invoke(tile);
        }

        public void HighlightGameTiles(List<Tile> tiles, int alternative = 0)
        {
            ClearTileHighlights();
            for (int i = 0; i < tiles.Count; i++)
            {
                var cell = tiles[i].Coordinates + gameRect.Position;
                SetCell(NAV_OVERLAY_LAYER, cell, NAV_OVERLAY_SOURCE, Vector2I.One, alternative);
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                var neighbors = tiles[i].GetNeighborsAsArray(false);
                // skip if all neighbors are there
                if (neighbors.Length == 4) { continue; }
                var mask = new Compass();
                for (int j = 0; j < neighbors.Length; j++)
                {
                    if (neighbors[j] != null)
                    {
                        mask |= (Compass)Mathf.Pow(2, j);
                    }
                }

                var cell = tiles[i].Coordinates + gameRect.Position;
                SetCell(NAV_OVERLAY_LAYER, cell, NAV_OVERLAY_SOURCE, Vector2I.One);
            }
        }

        public void ClearTileHighlights()
        {
            ClearLayer(NAV_OVERLAY_LAYER);
        }
        
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

