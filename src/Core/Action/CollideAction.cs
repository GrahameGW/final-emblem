namespace FinalEmblem.Core
{
    public class CollideAction : IUnitAction
    {
        public Unit Unit { get; set; }

        private readonly Tile obstacle;

        public CollideAction(Unit unit, Tile obstacle)
        {
            this.obstacle = obstacle;
            Unit = unit;
        }

        public void Execute() 
        {
            Unit.HasActed = true;
        }
    }
}

