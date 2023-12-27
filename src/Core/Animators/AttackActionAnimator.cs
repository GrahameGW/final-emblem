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
        private AttackAnimationDisplay attackDisplay;

        public AttackActionAnimator(AttackAction action, AttackAnimationDisplay display)
        {
            actor = action.Actor;
            target = action.Target;
            initialTargetHp = target.HP;
            attackDisplay = display;
        }

        public async void ReceiveActionResult(IActionResult result)
        {
            var unitTile = actor.Tile;
            attackDisplay.SetActors(actor, target);


            var anchorPos = unitTile.DirectionToApproxDiagonals(target.Tile, true) switch
            {
                Compass.N => target.Tile.East.WorldPosition.Vector2XY(),
                Compass.S => unitTile.East.WorldPosition.Vector2XY(),
                Compass.E => unitTile.Southwest.WorldPosition.Vector2XY(),
                Compass.W => target.Tile.Southwest.WorldPosition.Vector2XY(),
                _ => throw new NotImplementedException()
            };
                
            attackDisplay.GlobalPosition = GetViewport().CanvasTransform * anchorPos;

            if (result is not AttackActionResult attackResult)
            {
                throw new ArgumentException($"Passed inappropriate action result to attack animator: {result}");
            }
            await attackDisplay.PlayAttack(attackResult);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

