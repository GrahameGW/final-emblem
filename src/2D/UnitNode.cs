using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    [GlobalClass]
    public partial class UnitNode : Node2D
    {
        [Export] public int Move { get; private set; }
        [Export] public FactionName Faction { get; private set; }

        public Unit Unit;
    }

    public enum FactionName
    {
        Player,
        Enemy,
        Other
    }
}

