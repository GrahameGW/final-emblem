namespace FinalEmblem.Core
{
    public class WaitAction : IAction
    {
        public Unit Actor { get; set; }

        public ActionResult Execute()
        {
            return new ActionResult(Actor, ActionResultId.Waited);
        }
    }
}

