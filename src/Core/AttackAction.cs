namespace FinalEmblem.Core
{
    public class AttackAction : IUnitAction
    {
        public Unit Target { get; private set; }
        public Unit Attacker { get; private set; }

        public AttackAction(Unit attacker, Unit target)
        {
            Target = target;
            Attacker = attacker;
        }

        public int Damage()
        {
            return Attacker.Attack;
        }
        public void Execute() 
        {
            Target.Damage(Damage());
            Attacker.HasActed = true;
        }
    }
}

