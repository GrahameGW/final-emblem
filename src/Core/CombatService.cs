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

        public static IEnumerator StartExecution(Unit unit, IAction action)
        {
            var routine = action.Execute(unit);
            // check for triggers and do stuff if triggered
            return routine;
        }

        public static IEnumerator NextStep(IEnumerator routine, Action onFinished = null)
        {
            if (!routine.MoveNext())
            {
                onFinished?.Invoke();
                return null;
            }

            return routine;
        }
    }
}
