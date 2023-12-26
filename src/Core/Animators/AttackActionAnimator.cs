using Godot;
using System;
using System.Linq;

namespace FinalEmblem.Core
{
    public partial class AttackActionAnimator : ActionAnimator, IAwaitActionResult
    {
        [Export] PackedScene attackDisplay;
        
        private Unit actor;
        private Unit target;
        private int initialTargetHp;

        public AttackActionAnimator(AttackAction action)
        {
            actor = action.Actor;
            target = action.Target;
            initialTargetHp = target.HP;
        }

        public void ReceiveActionResult(ActionResult result)
        {
            var unitTile = actor.Tile;

        }

        // currently only doing the first attack
        public override void _EnterTree()
        {
            GD.Print($"{actor} did an attack! Hurt {target}");
            var display = attackDisplay.Instantiate<AttackAnimationDisplay>();
            display.OnDisplayChangeComplete += DisplayChangeCompleteHandler;
            AddChild(display);
        }

        private void DisplayChangeCompleteHandler()
        {
            EmitSignal(AnimCompleteSignal);
        }
    }

    public partial class AttackAnimationDisplay : Control
    {
        [Export] Label attackerName;
        [Export] Label targetName;
        [Export] ProgressBar attackerHealth;
        [Export] ProgressBar targetHealth;

        public event Action OnDisplayChangeComplete;

        private Unit attacker, defender;

        public void SetActors(Unit attacker, Unit defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }

        public override void _EnterTree()
        {
        }
    }
}

