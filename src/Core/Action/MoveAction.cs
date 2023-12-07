
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class MoveAction : IAction
    {
        public readonly List<Tile> Path;

        public MoveAction(List<Tile> path)
        {
            Path = path;
        }

        public void Execute(Unit unit) 
        { 
            foreach (var tile in Path)
            {
                unit.Tile = tile;
            }
        }
    }
}

