using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public interface IAction
    {
        void Execute(Unit unit);
    }

    public struct ActionResult
    {
        public ResultType type;
        public Unit actor;
        public List<Tile> affected;
    }

    public enum ResultType
    {
        Waited,
        Moved,
        LostHp,
        Died
    }
}

