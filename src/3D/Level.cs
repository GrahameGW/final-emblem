using Godot;

namespace FinalEmblem.Core3D
{
    public partial class Level : Node
    {
        [Export] Vector2I mapSize;

        private Grid grid;

        public override void _Ready()
        {
            grid = GetNode<Grid>("Grid");
            grid.Initialize(mapSize);
        }
    }
}

