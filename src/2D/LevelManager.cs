using Godot;
using FinalEmblem.Core;
using System.Linq;

namespace FinalEmblem.QueryModel
{
    public partial class LevelManager : Node
    {
        public Level Level { get; private set; }

        private GameMap gameMap;
        private TacticsController tactics;
        private TokenController tokens;
        private LevelHUD hud;

        public override void _Ready()
        {
            tactics = GetNode<TacticsController>("Tactics");
            tokens = GetNode<TokenController>("Tokens");
            gameMap = GetNode<GameMap>("GameMap");
            hud = GetNode<LevelHUD>("HUD");

            var grid = gameMap.GenerateGridFromMap();
            tokens.Initialize();
            gameMap.SetUnitPositionsFromTokens(tokens.Tokens);
            var units = tokens.Units;

            Level = new Level(grid, units);
            tactics.Initialize(gameMap, Level, tokens);
            hud.Initialize(Level, gameMap, tactics);

            NavService.SetGridInstance(grid);
            CombatService.SetLevelInstance(Level);

            Level.OnGameEnded += EndGameHandler;

            Level.StartTurn(Faction.Player);

        }

        public void EndTurn()
        {
            Level.EndTurn();
        }

        private void EndGameHandler(Faction winner)
        {
            Level.OnGameEnded -= EndGameHandler;

            GD.Print($"Winner: {winner}");
            tactics.QueueFree();
        }
    }
}

