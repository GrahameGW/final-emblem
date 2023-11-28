using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerController : Node
    {
        private GameMap map;
        [Export] bool isActing;

        public void Initialize(GameMap gameMap)
        {
            map = gameMap;
            Level.OnTurnStarted += FactionTurnStartHandler;
        }

        public override void _Input(InputEvent input)
        {
            if (input is InputEventMouseButton && input.IsPressed())
            {
                var pos = map.GetGlobalMousePosition();
                var tile = map.GetGridTile(pos);

                if (tile.Unit != null && isActing)
                {
                    GD.Print($"{tile.Coordinates} has a unit: {tile.Unit}");
                    var inRange = NavService.FindTilesInRange(5, tile, includeStart: false);
                    map.HighlightGameTiles(inRange);
                }
            }
        }

        private void FactionTurnStartHandler(Faction faction)
        {
            isActing = faction == Faction.Player;
        }
    }
}

