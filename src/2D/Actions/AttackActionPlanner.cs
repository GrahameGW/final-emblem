using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using System;

namespace FinalEmblem.Godot2D
{
    public partial class AttackActionPlanner : Node, IActionPlanner
    {
        public Action<List<IAction>> OnActionsBuilt { get; set; }
        private PlayerController player;
        private List<Tile> inRange;
        private GameMap map;

        private const int TILE_ALT_ID = 1;


        public override void _Ready()
        {
            player = GetParent<PlayerController>();
            var currentTile = player.SelectedTile;
            inRange = NavService.FindTilesInRange(1, currentTile, includeStart: false);
            map = player.Map;
            map.HighlightGameTiles(inRange, TILE_ALT_ID);
        }

        public void HandleInput(InputEvent input)
        {
            bool clickEvent = input is InputEventMouseButton && input.IsPressed();
            var underMouse = player.GetTileUnderMouse();
            if (clickEvent && inRange.Contains(underMouse))
            {
                // hard code attack enemy faction
                if (underMouse.Unit?.Faction != Faction.Player)
                {
                    var actions = new List<IAction> { new AttackActionOld(underMouse.Unit) };
                    OnActionsBuilt?.Invoke(actions);
                }
            }
        }

        public override void _ExitTree()
        {
            map.ClearTileHighlights();
        }
    }
}

