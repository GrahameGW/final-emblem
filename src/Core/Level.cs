using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace FinalEmblem.Core
{
    public class Level
    {
        public int Round { get; private set; }
        public Faction CurrentFaction { get; private set; }
        public List<Faction> Factions { get; private set; }
        public List<Unit> Units { get; private set; }
        public List<Unit> ActingUnits { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public static Action<Faction> OnTurnEnded;
        public event Action<Faction> OnGameEnded;

        public readonly Grid grid;
        private readonly IVictoryCondition[] victories;

        public Level(Grid grid, IVictoryCondition[] wins, List<Unit> units, List<Faction> factions)
        {
            this.grid = grid;
            victories = wins;
            Units = units;
            Factions = factions;
            Round = 0;
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
            if (faction == Factions[0])
            {
                Round += 1;
            }
        }

        public void EndTurn()
        {
            GD.Print($"Ending turn for {CurrentFaction}");
            OnTurnEnded?.Invoke(CurrentFaction);
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

        public void EndGameIfFactionWon(out Faction? winner)
        {
            winner = null;

            if (!Units.Any(u => u.Faction == Faction.Player))
            {
                winner = Faction.Enemy;
            }
            else if (!Units.Any(u => u.Faction == Faction.Enemy))
            {
                winner = Faction.Player;
            }
            
            if (winner != null)
            {
                EndTurn();
                OnGameEnded?.Invoke((Faction) winner);
            }
        }
    }
}
