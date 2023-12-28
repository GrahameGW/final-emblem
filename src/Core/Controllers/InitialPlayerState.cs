namespace FinalEmblem.Core
{
    public class InitialPlayerState : PlayerState 
    {
        private Tile selectedTile;

        public InitialPlayerState(Tile selected = null)
        {
            selectedTile = selected;
        }

        public override void EnterState(PlayerController context)
        {
            _context = context;
            SelectedTileChangedHandler(selectedTile);
            _context.Map.OnSelectedTileChanged += SelectedTileChangedHandler;
        }

        public override void ExitState()
        {
            _context.Map.ClearTileHighlights();
            _context.Map.OnSelectedTileChanged -= SelectedTileChangedHandler;
        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            selectedTile = tile;
            _context.SelectedTile = tile;
            bool tileIsEmpty = tile == null || tile.Unit == null;
            if (tileIsEmpty || tile.Unit.Faction != Faction.Player || tile.Unit.HasMoved)
            {
                _context.Map.ClearTileHighlights();
            }
            else
            {
                var inRange = NavService.FindAvailableMoves(tile.Unit.Move, tile);
                _context.Map.HighlightGameTiles(inRange);
            }
        }
    }
}

