using Godot;
using System;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class AttackActionAnimator : ActionAnimator, IAwaitActionResult
    {       
        private Unit actor;
        private Unit target;
        private int initialTargetHp;
        private PackedScene attackDisplay;

        public AttackActionAnimator(AttackAction action, PackedScene display)
        {
            actor = action.Actor;
            target = action.Target;
            initialTargetHp = target.HP;
            attackDisplay = display;
        }

        // currently only doing the first attack
        public override void _EnterTree()
        {
            GD.Print($"{actor} did an attack! Hurt {target}");
            var display = attackDisplay.Instantiate<AttackAnimationDisplay>();
            AddChild(display);
        }

        public async void ReceiveActionResult(IActionResult result)
        {
            var unitTile = actor.Tile;
            var dir = unitTile.DirectionToApproxDiagonals(target.Tile);
            var display = GetChild<AttackAnimationDisplay>(0);
            display.SetActors(actor, target);

            if (dir == Compass.N || dir == Compass.S)
            {
                // TODO: get position in viewport for left/right
                display.GlobalPosition = unitTile.East.WorldPosition.Vector2XY();
            }
            else
            {
                // todo: get position in viewport for up/down
                display.GlobalPosition = unitTile.South.WorldPosition.Vector2XY();
            }

            if (result is not AttackActionResult attackResult)
            {
                throw new ArgumentException($"Passed inappropriate action result to attack animator: {result}");
            }
            await display.PlayAttack(attackResult);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

