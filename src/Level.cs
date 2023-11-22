using Godot;

namespace FinalEmblem.Core
{
    public partial class Level : Node
    {
        private Environment environment;
        private UnitManager unitManager;

        public override void _Ready()
        {
            environment = GetNode<Environment>("Environment");

            unitManager = GetNode<UnitManager>("Units");
            unitManager.Grid = environment.Grid;
            unitManager.AddUnit(UnitType.Barbarian, new Vector2I(7, 8));

        }
    }
}

