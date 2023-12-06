
namespace FinalEmblem.Core
{
    public class MoveAction : IAction
    {
        public readonly Tile from;
        public readonly Tile to;

        public MoveAction(Tile from, Tile to)
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

    }
}

