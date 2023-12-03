using System;
using System.Collections;

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
            unit.StartActionPlayback(action);
            return true;  // return false if fails later
        }
    }
}
