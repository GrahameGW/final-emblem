using Godot;
using FinalEmblem.Core;
using System;
using FinalEmblem.src.Query.Designers;

namespace FinalEmblem.QueryModel
{
    public partial class TacticsController : Node
    {
        public Level Level { get; private set; }
        public TokenController Tokens { get; private set; }
        public GameMap Map { get; private set; }
        public bool CanAcceptInput { get; set; }

        [Export] PackedScene moveDesigner;

        private TacticsState state;

        public void Initialize(GameMap gameMap, Level level, TokenController tokens)
        {
            Level = level;
            Map = gameMap;
            Tokens = tokens;
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
            if (CanAcceptInput)
            {
                state.HandleInput(input);
            }
        }

        public void BuildTacticDesigner(UnitTactic actionName, Unit unit)
        {
            ITacticDesigner designer = actionName switch
            {
                UnitTactic.Move => InstantiateMoveDesigner(unit.Tile),
                UnitTactic.Attack => InstantiateAttackDesigner(unit),
                UnitTactic.Wait => new WaitTacticDesigner(unit),
                _ => throw new ArgumentOutOfRangeException(actionName.ToString())
            };

            ChangeState(new ActionDesignTacticsState(this, designer, unit));
        }

        private MoveTacticDesigner InstantiateMoveDesigner(Tile start)
        {
            var designer = moveDesigner.Instantiate<MoveTacticDesigner>();
            designer.Initialize(start, Map);
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
            CanAcceptInput = faction == Faction.Player;
            if (CanAcceptInput)
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

