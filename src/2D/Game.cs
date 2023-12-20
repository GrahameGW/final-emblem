using Godot;
using System.Collections.Generic;
using System.Linq;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class Game : Node
    {
        public Level Level { get; private set; }

        private List<ControllerBase> controllers;
        private MidTurnController midTurnController;
        private int activeControllerIndex;
        private ControllerBase activeController;

        // private LevelHUD hud;

        public override void _Ready()
        {
            // Get nodes already children of this node
            //tactics = GetNode<TacticsController>("Tactics");
            //tokens = GetNode<TokenController>("Tokens");
            var gameMap = GetNode<GameMap>("GameMap");
            var hud = GetNode<LevelHUD>("HUD");
            var units = gameMap.FindNodesOfType<Unit>();

            // Generate Grid and game data to build level with
            var grid = gameMap.GenerateGridFromMap();
            var factions = units.Select(u => u.Faction).Distinct().ToList();
            var victories = factions.Select(f => new KillAllOthersVictory(f)).ToArray();

            Level = new Level(grid, victories, units, factions);

            // Set up controllers 
            midTurnController = new MidTurnController(Level);
            controllers = InitializeControllers(factions, Level);

            //tactics.Initialize(gameMap, Level, tokens);
            //hud.Initialize(Level, gameMap, tactics);

            // Set up services
            NavService.SetGridInstance(grid);
            CombatService.SetLevelInstance(Level);

            // load MidTurn Controller which kicks off game state machine and starts the level
            activeController = midTurnController;
            activeControllerIndex = 0;
            LoadActiveController();
        }

        private List<ControllerBase> InitializeControllers(IEnumerable<Faction> factions, Level level)
        {
            var player = new PlayerController(level);
            var enemy = new AiController(level, Faction.Enemy);
            var controllers = new List<ControllerBase> { player, enemy };
            while (controllers.Count < factions.Count())
            {
                controllers.Add(new AiController(level, Faction.Other));
            }

            return controllers;
        }

        private void LoadActiveController()
        {
            activeController.OnControllerExited += ControllerExitHandler;
            AddChild(activeController);
        }

        private void ControllerExitHandler()
        {
            activeController.OnControllerExited -= ControllerExitHandler;

            if (activeController == midTurnController)
            {
                activeControllerIndex = controllers.NextOrFirstIndex(activeControllerIndex);
                activeController = controllers[activeControllerIndex];
            }
            else
            {
                activeController = midTurnController;
            }
        }

        private void EndGameHandler(Faction winner)
        {
            GD.Print($"Winner: {winner}");
           // tactics.QueueFree();
        }
    }
}

