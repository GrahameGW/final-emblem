using System;
using System.Collections;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public static class CombatService
    {
        private static Level level;

        public static void SetLevelInstance(Level instance)
        {
            level = instance;
        }

        public static List<IUnitAction> CalculateMoveImplications(Unit unit, MoveAction move)
        {
            for (int i = 0; i < move.Path.Count; i++)
            {
                if (move.Path[i].Unit == null || move.Path[i].Unit == unit) 
                { 
                    continue; 
                }

                var changedPath = new List<Tile>();
                changedPath.AddRange(move.Path);
                changedPath.RemoveRange(i, changedPath.Count - i);
                var changedMove = new MoveAction(unit, changedPath);
                var collision = new CollideAction(unit, move.Path[i]);
                var wait = new WaitAction(unit);
                return new List<IUnitAction> { changedMove, collision, wait };
            }

            return new List<IUnitAction> { move };
        }

        public static List<IUnitAction> CalculateAttackImplications(Unit unit, AttackAction attack)
        {
            var actuals = new List<IUnitAction> { attack };
            if (attack.Damage() >= attack.Target.HP)
            {
                actuals.Add(new DeathAction(attack.Target, level.RemoveUnit));
            }
            actuals.Add(new WaitAction(unit));
            return actuals;
        }

        public static List<IUnitAction> CalculateWaitImplications(Unit unit)
        {
            return new List<IUnitAction> { new WaitAction(unit) };
        }

    }
}
