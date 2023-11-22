using Godot;

namespace FinalEmblem.Core
{
    public partial class UnitManager : Node
    {
        [Export] PackedScene barbarian;

        public Grid Grid { get; set; }

        public void AddUnit(UnitType unitType, Vector2I coord)
        {
            var unit = barbarian.Instantiate() as Unit;
            AddChild(unit);
            Grid.GetTile(coord).Unit = unit;
        }
    }

    public enum UnitType
    {
        Barbarian
    }
}

