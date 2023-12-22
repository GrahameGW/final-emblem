using System;
using System.Collections;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public static class CombatService
    {
        private static Game level;

        public static void SetLevelInstance(Game instance)
        {
            level = instance;
        }

        public static List<IAction> CalculateActionImplications(Unit actor, IAction action)
        {
            var actuals = new List<IAction> { action };

            if (action is MoveAction move)
            {
                for (int i = 0; i < move.Path.Count; i++)
                {
                    if (move.Path[i].Unit == null) { continue; }

                    if (move.Path[i].Unit?.Faction != actor.Faction)
                    {
                        var changedPath = new List<Tile>();
                        changedPath.AddRange(move.Path);
                        changedPath.RemoveRange(i, changedPath.Count - i);
                        var changedMove = new MoveAction(actor, changedPath);
                        var collision = new CollideAction(move.Path[i]);
                        actuals = new List<IAction> { changedMove, collision };
                        return actuals;
                    }
                }
            }
            else if (action is AttackAction attack)
            {
                if (attack.Target.HP <= actor.Attack)
                {
                    actuals.Add(KillUnit(attack.Target));
                }

                actuals.Add(new WaitAction { Actor = actor });
                return actuals;
            }

            return actuals;
        }

        private static DeathAction KillUnit(Unit deceased)
        {
            return new DeathAction
            {
                Actor = deceased,
                OnDeathCallback = level.RemoveUnit
            };
        }
    }
}
