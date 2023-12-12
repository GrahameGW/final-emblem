namespace FinalEmblem.Core
{
    public class DeathAction : IAction
    {
        public Unit Actor { get; set; }
        
        public ActionResult Execute()
        {
            return new ActionResult(Actor, ActionResultId.Died);
        }
    }
}

