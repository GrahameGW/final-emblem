namespace FinalEmblem.Core
{
    public class CollideAction : IAction
    {
        public ActionType Type => ActionType.Collide;
        public Unit Actor { get; set; }

        private readonly Tile obstacle;

        public CollideAction(Tile obstacle)
        {
            this.obstacle = obstacle;
        }

        public ActionResult Execute()
        {
            return new ActionResult(Actor, ActionResultId.Collided);
        }
    }
}

