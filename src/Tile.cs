using Godot;


namespace FinalEmblem.Core
{
    public class Tile
    {
        public Vector2I Coordinates { get; private set; }
        public TileTerrain Terrain { get; private set; }
        public Unit Unit { get; set; }
        public IInteractable Interactable { get; private set; }

        public Tile(int x, int y, TileTerrain terrain)
        {
            Coordinates = new Vector2I(x, y);
            Terrain = terrain;
        }
    }
}
