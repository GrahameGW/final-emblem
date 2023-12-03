using Godot;
using FinalEmblem.Core;
using System;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerController : Node
    {
        public GameMap Map { get; private set; }
        public Node ActiveActionPlanner { get; private set; }
        public UnitGroup UnitGroup { get; private set; }
        public Tile SelectedTile
        {
            get => _selectedTile;
            set
            {
                _selectedTile = value;
                OnSelectedTileChanged?.Invoke(value);
            }
        }

        [Export] public bool IsActing;
        [Export] PackedScene movePlanner;
        [Export] CanvasLayer hudLayer;

        private PlayerControlState state;
        private Tile _selectedTile;


        public event Action<Tile> OnSelectedTileChanged;
        public event Action<IActionPlanner> OnActionPlanningStarted;


        public void Initialize(GameMap gameMap, UnitGroup units)
        {
            Map = gameMap;
            UnitGroup = units;
            state = new OtherTurnPCS(this);
            state.EnterState();
        }

        public void ChangeState(PlayerControlState next)
        {
            state.ExitState();
            next.EnterState();
            state = next;
        }

        public override void _UnhandledInput(InputEvent input)
        {
            state.HandleInput(input);
        }

        public Tile GetTileUnderMouse()
        {
            var pos = Map.GetGlobalMousePosition();
            return Map.GetGridTile(pos);
        }

        public void StartActionPlanning(Unit unit, UnitAction actionName)
        {
            IActionPlanner planner = actionName switch
            {
                UnitAction.Move => movePlanner.Instantiate<MoveActionPlanner>(),
                _ => throw new ArgumentOutOfRangeException(actionName.ToString())
            };
            ActiveActionPlanner = planner as Node;
            AddChild(ActiveActionPlanner);
            OnActionPlanningStarted?.Invoke(planner);
        }
    }
}

