using Godot;
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
        public static Action<Faction> OnTurnEnded;

        private List<List<Unit>> factions = new();
        private readonly Grid grid;

        public Level(Grid grid, List<List<Unit>> factions)
        {
            this.grid = grid;
            this.factions = factions;
            Units = factions.SelectMany(u => u).ToList();
            ActionableUnits = new();
        }

        public void StartTurn(Faction faction, List<Unit> units)
        {
            CurrentFaction = faction;
            ActionableUnits = units;

            for (int i =  0; i < ActionableUnits.Count; i++)
            {
                ActionableUnits[i].HasMoved = false;
                ActionableUnits[i].HasActed = false;
                ActionableUnits[i].OnUnitHasActedChanged += _ => UnitActedHandler();
                ActionableUnits[i].OnUnitDied += UnitDiedHandler;
            }

            OnTurnStarted?.Invoke(CurrentFaction);
            GD.Print($"Starting turn for {CurrentFaction}");
        }

        public void EndTurn()
        {
            GD.Print($"Ending turn for {CurrentFaction}");
            for (int i = 0; i < ActionableUnits.Count; i++)
            {
                ActionableUnits[i].HasMoved = false;
                ActionableUnits[i].HasActed = false;
                ActionableUnits[i].OnUnitHasActedChanged -= _ => UnitActedHandler();
                ActionableUnits[i].OnUnitDied -= UnitDiedHandler;
            }

            OnTurnEnded?.Invoke(CurrentFaction);
        }

        public void StartNextTurn()
        {
            var current = factions.First(u => u[0].Faction == CurrentFaction);
            int idx = factions.IndexOf(current);
            idx = idx == factions.Count - 1 ? 0 : idx + 1;
            StartTurn(factions[idx][0].Faction, factions[idx]);
        }

        public void UnitActedHandler() 
        { 
            if (ActionableUnits.Find(u => !u.HasActed) == null)
            {
                EndTurn();
            }
        }
        public void UnitDiedHandler(Unit deceased)
        {
            Units.Remove(deceased);
            var faction = deceased.Faction;
            var deceasedFaction = factions.First(u => u[0].Faction == faction);
            deceasedFaction.Remove(deceased);

            if (CurrentFaction == faction)
            {
                ActionableUnits.Remove(deceased);
            }

            if (deceasedFaction.Count == 0)
            {
                GD.Print($"Faction Eliminated: {faction}");
            }
        }
    }
}
