using System;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class AttackActionAnimator : OneShotAnimation
    {       
        private Unit actor;
        private Unit target;
        private AttackAction attack;
        private AttackAnimationDisplay attackDisplay;

        public AttackActionAnimator(AttackAction action, AttackAnimationDisplay display)
        {
            actor = action.Attacker;
            target = action.Target;
            attack = action;
            attackDisplay = display;
        }

        public override async void _EnterTree()
        {
            var unitTile = actor.Tile;
            var targetDir = unitTile.DirectionToApproxDiagonals(target.Tile, true);
            attackDisplay.SetActors(actor, target);

            var anchorPos = targetDir switch
            {
                Compass.N => target.Tile.East.WorldPosition.Vector2XY(),
                Compass.S => unitTile.East.WorldPosition.Vector2XY(),
                Compass.E => unitTile.Southwest.WorldPosition.Vector2XY(),
                Compass.W => target.Tile.Southwest.WorldPosition.Vector2XY(),
                _ => throw new NotImplementedException()
            };
                
            actor.SetIdleAnimation(targetDir);
            attackDisplay.GlobalPosition = GetViewport().CanvasTransform * anchorPos;

            AddChild(attackDisplay);
            await attackDisplay.PlayAttack(attack.Damage());
            EmitSignal(AnimCompleteSignal);
            attackDisplay.QueueFree();
        }
    }
}

