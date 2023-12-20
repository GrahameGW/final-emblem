using Godot;
using System;

namespace FinalEmblem.Core
{
    public partial class PlayerController : ControllerBase
    {
        public PlayerState State { get; private set; }

        public readonly GameMap map;

        [Export] PackedScene moveDesigner;

        public PlayerController(Level level, GameMap map) : base(level) 
        {
            Faction = Faction.Player;
            this.map = map;
        }
        
        public override void _EnterTree()
        {
            level.StartTurn(Faction);
            State = new InitialPlayerState();
            State.EnterState(this);
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
                UnitTactic.Move => InstantiateMoveDesigner(unit.Tile),
                UnitTactic.Attack => InstantiateAttackDesigner(unit),
                UnitTactic.Wait => new WaitTacticDesigner(unit),
                _ => throw new ArgumentOutOfRangeException(actionName.ToString())
            };

            ChangeState(new ActionDesignPlayerState(designer, unit));
        }

        private MoveTacticDesigner InstantiateMoveDesigner(Tile start)
        {
            var designer = moveDesigner.Instantiate<MoveTacticDesigner>();
            designer.Initialize(start, map);
            return designer;
        }

        private AttackTacticDesigner InstantiateAttackDesigner(Unit attacker)
        {
            var designer = new AttackTacticDesigner();
            designer.Initialize(attacker, map);
            return designer;
        }
    }
}

