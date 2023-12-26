using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class AttackAction : IAction
    {
        public ActionType Type => ActionType.Attack;
        public Unit Actor { get; set; }
        public readonly Unit Target;

        public AttackAction(Unit actor, Unit target)
        {
            Actor = actor;
            Target = target;
        }

        public IActionResult Execute()
        {
            var damage = Actor.Attack;
            Target.Damage(damage);
            return new AttackActionResult(this, damage);
        }
    }
}

