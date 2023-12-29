namespace FinalEmblem.Core
{
    public class CollideActionOld : IAction
    {
        public ActionType Type => ActionType.Collide;
        public Unit Actor { get; set; }

        private readonly Tile obstacle;

        public CollideActionOld(Tile obstacle)
        {
            this.obstacle = obstacle;
        }

        public IActionResult Execute()
        {
            return new BaseActionResult(Actor);
        }
    }
}

