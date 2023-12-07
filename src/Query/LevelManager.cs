using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System.Linq;

namespace FinalEmblem.QueryModel
{
    public partial class LevelManager : Node
    {
        private GameMap gameMap;
        //private Grid grid;
        private QueryLevel level;
        private TacticsController tactics;
        private LevelHUD hud;

        public override void _Ready()
        {
            tactics = GetNode<TacticsController>("Tactics");
            gameMap = GetNode<GameMap>("GameMap");
            hud = GetNode<LevelHUD>("HUD");

            var grid = gameMap.GenerateGridFromMap();
            var units = gameMap.GenerateUnitsFromMap();
            grid.SetUnitList(units);

            level = new QueryLevel(grid, units);
            tactics.Initialize(gameMap, level);
            hud.Initialize(level, gameMap, tactics);

            NavService.SetGridInstance(grid);
            CombatService.SetGridInstance(grid);

            level.StartTurn(Faction.Player, units);

/*
            player = GetNode<PlayerController>("Player");
            var playerUnits = factions.FirstOrDefault(f => f.Units[0].Faction == Faction.Player);
            player.Initialize(gameMap, playerUnits, level);

            level.StartTurn(Faction.Player, factions[0].Units);
*/
        }

        public void EndTurn()
        {
            level.EndTurn();
        }
    }
}

