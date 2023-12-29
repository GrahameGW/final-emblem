namespace FinalEmblem.Core
{
    public class WaitAction : IUnitAction
    {
        public Unit Unit { get; private set; }

        public WaitAction(Unit unit)
        {
            Unit = unit;
        }

        public void Execute() 
        {
            Unit.HasActed = true;
            Unit.HasMoved = true;
        }
    }
}

