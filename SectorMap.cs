using Godot;
using System;

namespace FinalEmblem.Core
{
	public partial class SectorMap : TileMap
	{
        // CDL = "Custom Data Layer"
        private const string CDL_SURFACE = "Surface";

        public override void _Ready()
        {
            ErrorOnMislabeledTilesets(0, CDL_SURFACE);
        }

        public override void _Process(double delta)
        {
            var mousePos = GetViewport().GetMousePosition();
            var tilePos = LocalToMap(mousePos);
            var tileData = GetCellTileData(0, tilePos);
            if (tileData != null)
            {
                GD.Print(tileData.GetCustomData("CDL_SURFACE"));
            }
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
