using Godot;
using System;

namespace FinalEmblem.Core
{
    public partial class PlayerController : ControllerBase
    {
        public override string DebugName => "PlayerController";

        public PlayerState State { get; private set; }
        public Game Level { get; private set; }
        public GameMap Map { get; private set; }
        public Tile SelectedTile
        {
            get => _selectedTile;
            set
            {
                _selectedTile = value;
                OnUnitSelected?.Invoke(value?.Unit);
            }
        }

        private Tile _selectedTile;

        public event Action<Unit> OnUnitSelected;

        public void Initialize(Game level, GameMap map)
        {
            Faction = Faction.Player;
            Level = level;
            this.Map = map;
        }
        
        public override void _EnterTree()
        {
            Level.OnTurnEnded += TurnEndedHandler;
            Level.StartTurn(Faction);
            State = new InitialPlayerState();
            State.EnterState(this);
            GD.Print("Entering InitialPlayerState");
        }

        public override void _UnhandledInput(InputEvent input)
        {
            State.HandleInput(input);
        }

        public override void _Process(double delta)
        {
            State.Update((float)delta);
        }

        public override void _ExitTree()
        {
            State.ExitState();
            State = null;
            Level.OnTurnEnded -=  TurnEndedHandler;
        }

        public void ChangeState(PlayerState next)
        {
            State.ExitState();
            State = next;
            GD.Print($"Entering state {next}");
            State.EnterState(this);
        }

        public void BuildTacticDesigner(UnitTactic actionName, Unit unit)
        {
            ITacticDesigner designer = actionName switch
            {
                UnitTactic.Move => DesignerService.GetMoveDesigner(unit.Tile, Map),
                UnitTactic.Attack => DesignerService.GetAttackTacticDesigner(unit, Map),
                UnitTactic.Wait => DesignerService.GetWaitTacticDesigner(unit),
                _ => throw new ArgumentOutOfRangeException(actionName.ToString())
            };

            ChangeState(new ActionDesignPlayerState(designer, unit));
        }

        private void TurnEndedHandler()
        {
            SelectedTile = null;
            ReleaseControl();
        }
    }
}

