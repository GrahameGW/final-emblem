
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class MoveAction : IAction
    {
        public Unit Actor { get; set; }
        public readonly List<Tile> Path;

        public MoveAction(Unit unit, List<Tile> path)
        {
            Actor = unit;
            Path = path;
        }

        public ActionResult Execute() 
        { 
            foreach (var tile in Path)
            {
                Actor.Tile = tile;
            }

            return new ActionResult
            {
                actor = Actor,
                result = ActionResultId.Moved,
                affected = Path
            };
        }
    }
}

