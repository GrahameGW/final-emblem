namespace FinalEmblem.Core
{
    public class RotateAction : IUnitAction
    {
        public Unit Unit { get; private set; }
        private readonly Compass direction;

        public RotateAction(Unit unit, Compass direction)
        {
            Unit = unit;
            this.direction = direction;
        }

        public void Execute()
        {
            Unit.Facing = direction;
            Unit.HasActed = true;
        }
    }
}

