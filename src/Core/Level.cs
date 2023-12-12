using Godot;
using System;
using System.Collections.Generic;
namespace FinalEmblem.Core
{
    public class Level
    {
        public Faction CurrentFaction { get; private set; }
        public List<Unit> Units { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public static Action<Faction> OnTurnEnded;

        private readonly Grid grid;

        public Level(Grid grid, List<Unit> units)
        {
            this.grid = grid;
            Units = units;
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

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
            // will have stuff about end of game logic and stuff here

        }
    }
}
