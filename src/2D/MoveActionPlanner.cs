using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System;

namespace FinalEmblem.Godot2D
{
    public partial class MoveActionPlanner : Line2D, IActionPlanner
    {
        public Action<List<IAction>> OnActionsBuilt { get; set; }

        private List<Tile> tilesInRange;
        private List<Tile> currentPath = new();
        private PlayerController player;
        private Tile currentTile;

        public override void _Ready()
        {
            player = GetParent<PlayerController>();
            var tile = player.SelectedTile;
            tilesInRange = NavService.FindTilesInRange(tile.Unit.Move, tile, includeStart: false);
        }

        public void HandleInput(InputEvent input)
        {
            var underMouse = player.GetTileUnderMouse();
            if (currentTile != underMouse)
            {
                ClearPoints();
                currentPath.Clear();
                currentTile = underMouse;
                if (tilesInRange.Contains(currentTile))
                {
                    UpdateCurrentPath(player.SelectedTile, currentTile);
                }
            }
            if (input is InputEventMouseButton && input.IsPressed())
            {
                if (currentPath == null) { return; }
                var actions = new List<IAction>();

                for (int i = 1; i < currentPath.Count; i++)
                {
                    actions.Add(new MoveAction(currentPath[i - 1], currentPath[i]));
                }

                OnActionsBuilt?.Invoke(actions);
            }
        }

        private void UpdateCurrentPath(Tile start, Tile end)
        {
            var path = NavService.FindShortestPath(start, end, tilesInRange);

            for (int i = 0; i < path.Count; i++)
            {
                AddPoint(path[i].WorldPosition.Vector2XY());
            }

            currentPath = path;
        }
    }
}

