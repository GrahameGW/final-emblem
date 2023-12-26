using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public interface IAction
    {
        ActionType Type { get; }
        Unit Actor { get; }
        ActionResult Execute();
    }

    public struct ActionResult
    {
        public Unit actor;
        public ActionResultId result;
        public List<Tile> affected;

        public ActionResult(Unit unit, ActionResultId outcome)
        {
            actor = unit;
            result = outcome;
            affected = new List<Tile> { unit.Tile };
        }
    }

    public enum ActionResultId
    {
        Waited,
        Moved,
        Attacked,
        Collided,
        Died
    }
}

