using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.Core
{
    public class Level
    {
        public Faction CurrentFaction { get; private set; }
        public List<Unit> Units { get; private set; }
        public List<Unit> ActionableUnits { get; private set; }

        public static Action<Faction> OnTurnStarted;

        private readonly Grid grid;

        public Level(Grid grid, List<Unit> units)
        {
            this.grid = grid;
            Units = units;
            ActionableUnits = new();
        }

        public void StartTurn(Faction faction)
        {
            CurrentFaction = faction;
            ActionableUnits = Units.Where(u => u.Faction == faction).ToList();
            OnTurnStarted?.Invoke(CurrentFaction);
        }
    }
}
