using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System;

namespace FinalEmblem.QueryModel
{
    public partial class MoveTacticDesigner : Line2D, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        private Tile startTile;
        private List<Tile> tilesInRange;
        private List<Tile> currentPath = new();
        private Tile currentTile;

        public void Initialize(Tile moveStart)
        {
            startTile = moveStart;
            tilesInRange = NavService.FindAvailableMoves(startTile.Unit.Move, startTile);
        }

        public void SetTileUnderMouse(Tile tile)
        {
            if (currentTile != tile)
            {
                ClearPoints();
                currentPath.Clear();
                currentTile = tile;
                if (tilesInRange.Contains(currentTile))
                {
                    UpdateCurrentPath(startTile, currentTile);
                }
            }
        }

        public void SetSelectedTile(Tile _)
        {
            if (currentPath == null) { return; }
            var action = new MoveAction(currentPath);
            OnActionBuilt?.Invoke(action);
            QueueFree();
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

