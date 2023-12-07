
namespace FinalEmblem.Core
{
    public class AttackActionOld : IAction
    {
        public Unit target;

        public AttackActionOld(Unit target)
        {
            this.target = target;
        }

        public void Execute(Unit unit)
        {
            target.Damage(unit.Attack);
        }
    }
}

