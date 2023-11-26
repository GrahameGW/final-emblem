using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class Unit : Node2D
    {
        public Tile Tile { get; set; }
        
        public int GetMoveCost(TerrainIndex terrain)
        {
            return 0;
        }
    }
}
