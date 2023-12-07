using Godot;
using FinalEmblem.Core;
using System;

namespace FinalEmblem.QueryModel
{
    public partial class TacticsController : Node
    {
        public QueryLevel Level { get; private set; }
        public GameMap Map { get; private set; }
        public bool IsActive { get; set; }

        [Export] PackedScene moveDesigner;

        private TacticsState state;

        public void Initialize(GameMap gameMap, QueryLevel level)
        {
            Level = level;
            Map = gameMap;
            state = new IdleTacticsState(this);

            Level.OnTurnStarted += TurnStartedHandler;
            Map.OnTileUnderMouseChanged += TileUnderMouseChangedHandler;
            Map.OnSelectedTileChanged += SelectedTileChangedHandler;
        }

        public override void _ExitTree()
        {
            Level.OnTurnStarted -= TurnStartedHandler;
            Map.OnTileUnderMouseChanged -= TileUnderMouseChangedHandler;
            Map.OnSelectedTileChanged -= SelectedTileChangedHandler;
        }

        public void ChangeState(TacticsState next)
        {
            GD.Print($"Exiting state {state}");
            state.ExitState();
            state = next;
            GD.Print($"Entering state {next}");
            state.EnterState();
        }

        public override void _UnhandledInput(InputEvent input)
        {
            if (IsActive)
            {
                state.HandleInput(input);
            }
        }

        public void BuildTacticDesigner(UnitAction actionName, Unit unit)
        {
            ITacticDesigner designer = actionName switch
            {
                UnitAction.Move => InstantiateMoveDesigner(unit.Tile),
                UnitAction.Attack => InstantiateAttackDesigner(unit),
                UnitAction.Wait => new WaitTacticDesigner(),
                _ => throw new ArgumentOutOfRangeException(actionName.ToString())
            };

            ChangeState(new ActionDesignTacticsState(this, designer, unit));
        }

        private MoveTacticDesigner InstantiateMoveDesigner(Tile start)
        {
            var designer = moveDesigner.Instantiate<MoveTacticDesigner>();
            designer.Initialize(start);
            return designer;
        }

        private AttackTacticDesigner InstantiateAttackDesigner(Unit attacker)
        {
            var designer = new AttackTacticDesigner();
            designer.Initialize(attacker, Map);
            return designer;
        }

        private void TurnStartedHandler(Faction faction)
        {
            IsActive = faction == Faction.Player;
            if (IsActive)
            {
                var entry = new IdleTacticsState(this);
                ChangeState(entry);
            }
        }

        private void TileUnderMouseChangedHandler(Tile tile) 
        {
            state.SetTileUnderMouse(tile);
        }

        private void SelectedTileChangedHandler(Tile tile) 
        {
            state.SetSelectedTile(tile);
        }
    }
}

