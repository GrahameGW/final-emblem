namespace FinalEmblem.Core
{
    public class Unit
    {
        public Tile Tile
        {
            get => _tile;
            set
            {
                _tile.Unit = this;
                _tile = value;
            }
        }
        public int Move { get; set; }

        private Tile _tile;
    }

}
