namespace FinalEmblem.Core
{
    public class WaitAction : IAction
    {
        public ActionType Type => ActionType.Wait;
        public Unit Actor { get; set; }

        public IActionResult Execute()
        {
            return new BaseActionResult(Actor);
        }
    }
}

