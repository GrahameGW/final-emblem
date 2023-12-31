using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class MoveAction : IUnitAction
    {
        public Unit Unit { get; private set; }
        public List<Tile> Path { get; private set; }

        public MoveAction(Unit unit, List<Tile> path)
        {
            Unit = unit;
            Path = path;
        }

        public void Execute()
        {
            for (int i = 1; i < Path.Count; i++)
            {
                Unit.Facing = Unit.Tile.DirectionToNeighbor(Path[i]);
                Unit.Tile = Path[i];
            }

            Unit.HasMoved = true;
        }
    }
}

