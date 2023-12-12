using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace FinalEmblem.Core
{
    public class Level
    {
        public Faction CurrentFaction { get; private set; }
        public List<Faction> Factions { get; private set; }
        public List<Unit> Units { get; private set; }
        public List<Unit> ActingUnits { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public static Action<Faction> OnTurnEnded;

        private readonly Grid grid;

        public Level(Grid grid, List<Unit> units)
        {
            this.grid = grid;
            Units = units;
            Factions = units.Select(u => u.Faction).Distinct().ToList();
        }

        public void StartTurn(Faction faction)
        {
            CurrentFaction = faction;
            OnTurnStarted?.Invoke(CurrentFaction);
            GD.Print($"Starting turn for {CurrentFaction}");
            ActingUnits = Units.Where(u => u.Faction == CurrentFaction).ToList();
            for (int i = 0; i < ActingUnits.Count; i++)
            {
                ActingUnits[i].HasActed = ActingUnits[i].HasMoved = false;
            }
        }

        public void EndTurn()
        {
            GD.Print($"Ending turn for {CurrentFaction}");
            OnTurnEnded?.Invoke(CurrentFaction);
        }

        public void NextTurn()
        {
            EndTurn();
            var index = Factions.IndexOf(CurrentFaction);
            index = index == Factions.Count - 1 ? 0 : index + 1;
            StartTurn(Factions[index]);
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
            // will have stuff about end of game logic and stuff here
        }


        public bool HaveAllUnitsActed()
        {
            return ActingUnits.All(u => u.HasActed);
        }
    }
}
