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
            if (tile?.Unit?.Faction == Faction.Player && !tile.Unit.HasMoved)
            {
                var inRange = NavService.FindAvailableMoves(tile.Unit.Move, tile);
                context.Map.HighlightGameTiles(inRange);
            }
            else
            {
                context.Map.ClearTileHighlights();
            }
        }
    }
}

