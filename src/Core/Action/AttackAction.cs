using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class AttackAction : IAction
    {
        public Unit Actor { get; set; }
        public readonly Unit Target;

        public AttackAction(Unit actor, Unit target)
        {
            Actor = actor;
            Target = target;
        }

        public ActionResult Execute()
        {
            Target.Damage(Actor.Attack);

            return new ActionResult
            {
                actor = Actor,
                result = ActionResultId.Attacked,
                affected = new List<Tile> { Target.Tile },
            };
        }
    }
}

