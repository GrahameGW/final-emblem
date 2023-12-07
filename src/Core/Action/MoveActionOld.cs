namespace FinalEmblem.Core
{
    public class MoveActionOld : IAction
    {
        public readonly Tile from;
        public readonly Tile to;

        public MoveActionOld(Tile from, Tile to)
        {
            this.from = from;
            this.to = to;
        }

        public void Execute(Unit unit)
        {
            unit.Tile = to;
        }
    }

    public class DeathAction : IAction
    {
        public void Execute(Unit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}

