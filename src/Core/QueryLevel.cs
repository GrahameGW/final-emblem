using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.Core
{
    public class QueryLevel
    {
        public Faction CurrentFaction { get; private set; }
        public List<Unit> Units { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public static Action<Faction> OnTurnEnded;

        private readonly Grid grid;

        public QueryLevel(Grid grid, List<Unit> units)
        {
            this.grid = grid;
            Units = units;

            for (int i = 0; i < units.Count; i++)
            {
                // units[i].OnUnitHasActedChanged += UnitActedHandler;
            }
        }

        public void StartTurn(Faction faction, List<Unit> units)
        {
            CurrentFaction = faction;
            Units = units;
            OnTurnStarted?.Invoke(CurrentFaction);
            GD.Print($"Starting turn for {CurrentFaction}");
        }

        public void EndTurn()
        {
            GD.Print($"Ending turn for {CurrentFaction}");
            OnTurnEnded?.Invoke(CurrentFaction);
        }

        public void NextTurn()
        {
            /*
            var current = factions.First(u => u[0].Faction == CurrentFaction);
            int idx = factions.IndexOf(current);
            idx = idx == factions.Count - 1 ? 0 : idx + 1;
            StartTurn(factions[idx][0].Faction, factions[idx]);
            */
        }

        /*
        public void UnitActedHandler()
        {
            if (ActionableUnits.Find(u => !u.HasActed) == null)
            {
                EndTurn();
            }
        }
        */
        public void UnitDiedHandler(Unit deceased)
        {
            Units.Remove(deceased);
            var faction = deceased.Faction;
            // var deceasedFaction = factions.First(u => u[0].Faction == faction);
            // deceasedFaction.Remove(deceased);

            if (CurrentFaction == faction)
            {
            }
            /*
            if (deceasedFaction.Count == 0)
            {
                GD.Print($"Faction Eliminated: {faction}");
            }
            */
        }
    }
}
