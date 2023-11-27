using Godot;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class Level
    {
        public readonly List<Faction> Factions = new();
        public int CurrentFaction { get; private set; }
        private readonly Grid grid;

        public Level(Grid grid, List<Faction> factions)
        {
            this.grid = grid;
            Factions = factions;
            Faction.OnTurnComplete += TurnCompleteHandler;

            CurrentFaction = 0;
            Factions[CurrentFaction].StartTurn();
        }

        ~Level()
        {
            Faction.OnTurnComplete -= TurnCompleteHandler;
            NavService.SetGridInstance(null);
        }

        private Faction InitFaction() 
        {
            // will later need params for player vs AI
            var unit = new Unit { Move = 5, Tile = grid.GetTile(5, 8) };
            var faction = new Faction();
            faction.Units.Add(unit);
            return faction;
        }

        private void TurnCompleteHandler(Faction factionDone)
        {
            CurrentFaction = CurrentFaction == Factions.Count - 1 ? 0 : CurrentFaction + 1;
            Factions[CurrentFaction].StartTurn();
        }
    }
}
