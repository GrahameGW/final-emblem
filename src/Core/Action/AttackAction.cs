using System;

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
            return Attacker.Strength + Attacker.Weapon.Damage;
        }

        public void Execute() 
        {
            Target.Damage(Damage());
            Attacker.HasActed = true;
            Attacker.Facing = Attacker.Tile.DirectionToApproxDiagonals(Target.Tile);
        }
    }
}

