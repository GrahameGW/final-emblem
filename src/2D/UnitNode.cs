using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    [GlobalClass]
    public partial class UnitNode : Node2D
    {
        [Export] public int Move { get; private set; }
        [Export] public Faction Faction { get; private set; }

        public Unit Unit { get; private set; }

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

