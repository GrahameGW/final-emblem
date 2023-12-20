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
            _context.map.OnSelectedTileChanged += SelectedTileChangedHandler;
        }

        public override void ExitState()
        {
            _context.map.ClearTileHighlights();
            _context.map.OnSelectedTileChanged -= SelectedTileChangedHandler;
        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            selectedTile = tile;
            bool tileIsEmpty = tile == null || tile.Unit == null;
            if (tileIsEmpty || tile.Unit.Faction != Faction.Player)
            {
                _context.map.ClearTileHighlights();
            }
            else
            {
                var inRange = NavService.FindAvailableMoves(tile.Unit.Move, tile);
                _context.map.HighlightGameTiles(inRange);
            }
        }
    }
}

