using Godot;


namespace FinalEmblem.Core
{
    public class Tile
    {
        public Vector2I Coordinates { get; private set; }
        public Vector2 GlobalPosition { get; private set; }
        public TileTerrain Terrain { get; private set; }
        public IInteractable Interactable { get; private set; }
        public Grid Grid { get; private set; }

        public Unit Unit { 
            get => _unit; 
            set
            {
                _unit = value;
                _unit.Tile = this;
                _unit.GlobalPosition = GlobalPosition;
            } 
        }
        private Unit _unit;

        public Tile(int x, int y, TileTerrain terrain)
        {
            Coordinates = new Vector2I(x, y);
            Terrain = terrain;
        }
        public Tile(Vector2I coordinates, TileTerrain terrain, Grid grid)
        {
            Coordinates = coordinates;
            Terrain = terrain;
            Grid = grid;
            GlobalPosition = grid.WorldOffset + coordinates * grid.CellSize + grid.CellSize * 0.5f;
        }
    }
}
