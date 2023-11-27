namespace FinalEmblem.Core
{
    public class Unit
    {
        public Tile Tile
        {
            get => _tile;
            set
            {
                _tile = value;
                _tile.Unit = this;
            }
        }
        public int Move { get; set; }
        public FactionName Faction { get; set; }

        private Tile _tile;
    }
}
