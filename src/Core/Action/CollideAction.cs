namespace FinalEmblem.Core
{
    public class CollideAction : IAction
    {
        private Tile obstacle;

        public CollideAction(Tile obstacle)
        {
            this.obstacle = obstacle;
        }

        public Unit Actor { get; set; }

        public ActionResult Execute()
        {
            return new ActionResult(Actor, ActionResultId.Collided);
        }
    }
}

