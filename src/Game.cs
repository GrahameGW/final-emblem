using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class Game : Node
    {
        public int Round { get; private set; }
        public Faction CurrentFaction { get; private set; }
        public List<Faction> Factions { get; private set; }
        public List<Unit> Units { get; private set; }
        public List<Unit> ActingUnits { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public event Action OnTurnEnded;
        public event Action<Faction> OnGameEnded;

        private List<ControllerBase> controllers;
        private MidTurnController midTurnController;
        private int activeControllerIndex;
        private ControllerBase activeController;

        private AnimationController animator;

        private IVictoryCondition[] victories;

        public override void _Ready()
        {
            // Get nodes already children of this node
            var gameMap = GetNode<GameMap>("GameMap");
            var hud = GetNode<LevelHUD>("HUD");
            animator = GetNode<AnimationController>("AnimationController");

            // Initialize anything that can be immediately initialized
            animator.Initialize(this);

            // Build level
            var grid = gameMap.GenerateGridFromMap();
            Units = gameMap.FindNodesOfType<Unit>();
            gameMap.SetUnitPositionsFromTokens(Units);
            Factions = Units.Select(u => u.Faction).Distinct().ToList();
            victories = Factions.Select(f => new KillAllOthersVictory(f)).ToArray();

            // Set up services
            NavService.SetGridInstance(grid);
            CombatService.SetLevelInstance(this);
            DesignerService.Initialize();

            // Set up controllers 
            midTurnController = new MidTurnController();
            var player = new PlayerController();
            var enemy = new AiController();
            player.Initialize(this, gameMap);
            enemy.Initialize(this, Faction.Enemy);
            controllers = new List<ControllerBase> { player, enemy };

            // Initialize anything left to do
            hud.Initialize(this, gameMap, player);


            // load PlayerController to start the game
            activeController = controllers[0];
            activeControllerIndex = 0;
            LoadActiveController();
        }

        private void LoadActiveController()
        {
            activeController.OnControllerExited += ControllerExitHandler;
            activeController.Name = activeController.DebugName;
            AddChild(activeController, true);
            GD.Print($"Active Controller: {activeController.Name}");
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

            LoadActiveController();
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
            OnTurnEnded?.Invoke();
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
            // will have stuff about end of game logic and stuff here
        }

        public void ExecuteActionQueue(List<IAction> actions, Action onActionsCompleted)
        {
            var results = new Queue<ActionResult>();
            foreach (var action in actions)
            {
                var res = action.Execute();
                results.Enqueue(res);
            }
            animator.AnimateActionResults(results, onActionsCompleted);
        }

        public bool HaveAllUnitsActed()
        {
            return ActingUnits.All(u => u.HasActed);
        }

        public void TestWinConditions()
        {
            for (int i = 0; i < victories.Length; i++)
            {
                var winner = victories[i].TestCondition(this);
                if (winner != null)
                {
                    OnGameEnded?.Invoke((Faction)winner);
                    EndGameHandler((Faction)winner);
                    break;
                }
            }
        }

        private void EndGameHandler(Faction winner)
        {
            GD.Print($"Winner: {winner}");
            // tactics.QueueFree();
        }
    }
}

