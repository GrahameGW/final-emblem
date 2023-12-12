using FinalEmblem.Core;


namespace FinalEmblem.QueryModel
{
    public class IdleTacticsState : TacticsState
    {
        public IdleTacticsState(TacticsController player) : base(player) 
        {
            player.Map.ClearTileHighlights();
        }

        public override void SetSelectedTile(Tile tile)
        {
            var isPlayer = tile?.Unit?.Faction == Faction.Player;
            if (isPlayer && !(tile.Unit.HasMoved || tile.Unit.HasActed))
            {
                var inRange = NavService.FindAvailableMoves(tile.Unit.Move, tile);
                context.Map.HighlightGameTiles(inRange);
            }
            else
            {
                context.Map.ClearTileHighlights();
            }
        }

        public override void ExitState()
        {
            context.Map.ClearTileHighlights();
        }
    }
}

