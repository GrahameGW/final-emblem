using System;
using System.Collections;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public static class CombatService
    {
        private static Grid grid;

        public static void SetGridInstance(Grid instance)
        {
            grid = instance;
        }

        public static bool TryExecution(Unit unit, IAction action)
        {
            action.Execute(unit);
            // check for triggers and do stuff if triggered 
            // all good for now
            return true;  // return false if fails later
        }

        private static void KillAnyDeadUnits()
        {
            for (int i = 0; i < grid.Tiles.Length; i++)
            {
                var unit = grid.Tiles[i].Unit;
                if (unit == null) { continue; }

                if (unit.HP <= 0)
                {
                    unit.ClearActions();
                    unit.EnqueueAction(new DeathAction());
                }
            }
        }

        public static List<IAction> CalculateActionImplications(IAction action)
        {
            // for action result framework
            return new List<IAction> { action };
        }
    }
}
