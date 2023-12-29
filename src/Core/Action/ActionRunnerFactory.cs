using Godot;
using System;

namespace FinalEmblem.Core
{
    public partial class ActionRunnerFactory : Resource
    {
        [Export] PackedScene attackAnimation;
        
        public ActionRunner Assemble(IUnitAction action)
        {
            if (action is MoveAction move)
            {
                return new ActionRunner(action, new MoveActionAnimator(move));
            }

            if (action is AttackAction attack)
            {
                var display = attackAnimation.Instantiate<AttackAnimationDisplay>();
                return new ActionRunner(action, new AttackActionAnimator(attack, display));
            }

            if (action is WaitAction wait)
            {
                return new ActionRunner(action, new WaitActionAnimator(wait.Unit));
            }

            if (action is DeathAction death)
            {
                return new ActionRunner(action, new DeathActionAnimator(death.Unit));
            }

            if (action is CollideAction collide)
            {
                throw new NotImplementedException(collide.ToString());
            }

            throw new NotImplementedException("Unknown action");
        }
    }
}

