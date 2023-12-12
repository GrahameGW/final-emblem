using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.QueryModel
{
    public partial class LevelManager : Node
    {
        private Level level;
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
            grid.SetUnitList(units);

            level = new Level(grid, units);
            tactics.Initialize(gameMap, level, tokens);
            hud.Initialize(level, gameMap, tactics);

            NavService.SetGridInstance(grid);
            CombatService.SetGridInstance(grid);

            level.StartTurn(Faction.Player, units);
        }

        public void EndTurn()
        {
            level.EndTurn();
        }
    }
}

