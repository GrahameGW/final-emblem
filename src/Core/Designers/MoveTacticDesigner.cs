using Godot;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System;

namespace FinalEmblem.Core
{
    public partial class MoveTacticDesigner : Line2D, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        private Tile startTile;
        private List<Tile> tilesInRange;
        private List<Tile> currentPath = new();
        private Tile currentTile;
        private GameMap map;

        public void Initialize(Tile moveStart, GameMap map)
        {
            startTile = moveStart;
            tilesInRange = NavService.FindAvailableMoves(startTile.Unit.Move, startTile);
            this.map = map;
        }

        public override void _EnterTree()
        {
            map.HighlightGameTiles(tilesInRange);
        }

        public override void _ExitTree()
        {
            map.ClearTileHighlights();
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
            if (currentPath == null || currentPath.Count == 0) { return; }
            currentPath.Insert(0, startTile);
            var action = new MoveAction(startTile.Unit, currentPath);
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

