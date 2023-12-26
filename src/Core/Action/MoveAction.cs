
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class MoveAction : IAction
    {
        public ActionType Type => ActionType.Move;
        public Unit Actor { get; set; }
        public readonly List<Tile> Path;

        public MoveAction(Unit unit, List<Tile> path)
        {
            Actor = unit;
            Path = path;
        }

        public IActionResult Execute() 
        { 
            foreach (var tile in Path)
            {
                Actor.Tile = tile;
            }

            return new BaseActionResult(Actor);
        }
    }
}

