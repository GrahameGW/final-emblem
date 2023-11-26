using System.Collections.Generic;
using Godot;

namespace FinalEmblem.Core
{
    public class Level
    {
        public readonly List<Faction> Factions = new();
        public int CurrentFaction { get; private set; }

        public Level()
        {
            Faction.OnTurnComplete += TurnCompleteHandler;
            var grid = new Grid(new Vector2I(15, 10), Vector3.Zero, new Vector2(16f, 16f));
            NavService.SetGridInstance(grid);
            Factions.AddRange(new List<Faction> { new(), new() });
            CurrentFaction = 0;
            Factions[CurrentFaction].StartTurn();
        }

        ~Level()
        {
            Faction.OnTurnComplete -= TurnCompleteHandler;
            NavService.SetGridInstance(null);
        }

        private void TurnCompleteHandler(Faction factionDone)
        {
            CurrentFaction = CurrentFaction == Factions.Count - 1 ? 0 : CurrentFaction + 1;
            Factions[CurrentFaction].StartTurn();
        }
    }

}
