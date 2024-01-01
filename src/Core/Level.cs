using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class Level : Node
    {
        public int Round { get; private set; }
        public Faction CurrentFaction { get; private set; }
        public Faction NextFaction { get; private set; }
        public List<Faction> Factions { get; private set; }
        public List<Unit> Units { get; private set; }
        public List<Unit> ActingUnits { get; private set; }
        public GameMap Map { get; private set; }

        public event Action<Faction> OnTurnStarted;
        public event Action OnTurnEnded;
        public event Action<Faction> OnGameEnded;

        private List<ControllerBase> controllers;
        private MidTurnController midTurnController;
        private int activeControllerIndex;
        private ControllerBase activeController;

        private GlobalAnimations animator;
        private IVictoryCondition[] victories;

        [Export] ActionRunnerFactory actionFactory;


        public override void _Ready()
        {
            // Get nodes already children of this node
            Map = GetNode<GameMap>("GameMap");
            var hud = GetNode<LevelHUD>("HUD");
            animator = GetNode<GlobalAnimations>("AnimationController");

            // Initialize anything that can be immediately initialized
            animator.Initialize(this);

            // Build level
            var grid = Map.GenerateGridFromMap();
            Units = Map.FindNodesOfType<Unit>();
            Map.SetUnitPositionsFromTokens(Units);
            Factions = Units.Select(u => u.Faction).Distinct().ToList();
            victories = Factions.Select(f => new KillAllOthersVictory(f)).ToArray();

            // Set up services
            NavService.SetGridInstance(grid);
            CombatService.SetLevelInstance(this);
            DesignerService.Initialize();
            DialogService.Initialize(this);

            // Set up controllers 
            midTurnController = new MidTurnController();
            var player = new PlayerController();
            var enemy = new AiController();
            player.Initialize(this, Map);
            enemy.Initialize(this, Faction.Enemy);
            midTurnController.Initialize(this, animator);
            controllers = new List<ControllerBase> { player, enemy };

            // Initialize anything left to do
            hud.Initialize(this, Map, player);


            // load PlayerController to start the game
            //activeController = controllers[0];
            activeController = midTurnController;
            activeControllerIndex = -1;
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
            NextFaction = Factions.NextOrFirst(faction);
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
            TestWinConditions();
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit);
            // will have stuff about end of game logic and stuff here
        }

        public async void ExecuteActionQueue(List<IUnitAction> actions, Action onActionsCompleted)
        {
            var runners = actions.Select(actionFactory.Assemble);
            
            foreach (var item in runners)
            {
                animator.AddChild(item);
                await item.Run();
                item.QueueFree();
            }

            onActionsCompleted?.Invoke();
        }

        public bool HaveAllUnitsActed()
        {
            return ActingUnits.All(u => u.HasActed);
        }

        public bool TestWinConditions()
        {
            for (int i = 0; i < victories.Length; i++)
            {
                var winner = victories[i].TestCondition(this);
                if (winner != null)
                {
                    OnGameEnded?.Invoke((Faction)winner);
                    EndGameHandler((Faction)winner);
                    return true;
                }
            }

            return false;
        }

        private void EndGameHandler(Faction winner)
        {
            GD.Print($"Winner: {winner}");
            var parent = GetParent<App>();
            if (parent != null)
            {
                parent.LoadMainMenu();
            }
        }
    }
}

