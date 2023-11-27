using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    [GlobalClass]
    public partial class UnitNode : Node2D
    {
        [Export] public int Move { get; private set; }
        [Export] public FactionName Faction { get; private set; }

        public Unit Unit { get; private set; }

        public override void _Input(InputEvent input)
        {
            if (input is InputEventMouseButton && input.IsPressed())
            {
                GD.Print($"Clicked on a unit on tile {Unit.Tile.Coordinates}");
            }
        }

        public void Initialize()
        {
            Unit = new Unit()
            {
                Move = Move,
                Faction = Faction
            };
        }
    }
}

