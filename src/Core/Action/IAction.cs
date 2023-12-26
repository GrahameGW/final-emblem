using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public interface IAction
    {
        ActionType Type { get; }
        Unit Actor { get; }
        IActionResult Execute();
    }

    public interface IActionResult
    {
        Unit Actor { get; }
    }

    public class BaseActionResult : IActionResult
    {
        public Unit Actor { get; private set; }

        public BaseActionResult(Unit unit)
        {
            Actor = unit;
        }
    }
}

