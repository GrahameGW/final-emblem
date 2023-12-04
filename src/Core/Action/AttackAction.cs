
namespace FinalEmblem.Core
{
    public class AttackAction : IAction
    {
        public Unit target;

        public AttackAction(Unit target)
        {
            this.target = target;
        }

        public void Execute(Unit unit)
        {
            target.Damage(unit.Attack);
        }
    }
}

