using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class UnitFactory : Node
    {
        [Export] PackedScene unit;

        public Node2D GetUnit()
        {
            Node2D u = (Node2D)unit.Instantiate();
            //AddChild(u);
            //u.GlobalPosition = new Vector2(tile.WorldPosition.X, tile.WorldPosition.Y);
            return u;
        }
    }
}

