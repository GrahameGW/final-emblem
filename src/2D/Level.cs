using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class Level : Node
    {
        private Environment environment;

        public override void _Ready()
        {
            environment = GetNode<Environment>("Environment");

            //unitManager = GetNode<UnitManager>("Units");
            //unitManager.Grid = environment.Grid;
            //unitManager.AddUnit(UnitType.Barbarian, new Vector2I(7, 8));

        }
    }
}

