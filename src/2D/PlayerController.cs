using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerController : Node
    {
        private GameMap map;
        private Tile tileUnderMouse;
        private List<Tile> tilesInRange;
        private Tile selectedTile;

        [Export] bool isActing;
        [Export] Line2D line;

        public void Initialize(GameMap gameMap)
        {
            map = gameMap;
            tilesInRange = new List<Tile>();
            Level.OnTurnStarted += FactionTurnStartHandler;
        }

        public override void _Input(InputEvent input)
        {
            tileUnderMouse = GetTileUnderMouse();
            if (tileUnderMouse == null || !isActing) { return; }

            if (input is InputEventMouseButton && input.IsPressed())
            {
                tilesInRange = NavService.FindTilesInRange(5, tileUnderMouse, includeStart: false);
                map.HighlightGameTiles(tilesInRange);
                selectedTile = tileUnderMouse;
            } else if (tilesInRange.Contains(tileUnderMouse) && selectedTile != null)
            {
                var path = NavService.FindShortestPath(selectedTile, tileUnderMouse, tilesInRange);
                line.ClearPoints();
                for (int i = 0; i < path.Count; i++)
                {
                    line.AddPoint(path[i].WorldPosition.Vector2XY());
                }
            } else
            {
                line.ClearPoints();
            }
        }

        private Tile GetTileUnderMouse()
        {
            var pos = map.GetGlobalMousePosition();
            return map.GetGridTile(pos);
        }

        private void FactionTurnStartHandler(Faction faction)
        {
            isActing = faction == Faction.Player;
        }


        public void ShowPathLine(List<Tile> tiles)
        {

        }
    }


}

