using Godot;

namespace FinalEmblem.Core
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
