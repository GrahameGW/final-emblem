using System;

namespace FinalEmblem.Core
{
    public class DeathAction : IUnitAction
    {
        private Action<Unit> onDeathCallback;
        public Unit Unit { get; private set; }

        public DeathAction(Unit unit, Action<Unit> deathCallback)
        {
            onDeathCallback = deathCallback;
            Unit = unit;
        }

        public void Execute()
        {
            onDeathCallback.Invoke(Unit);
        }
    }
}

