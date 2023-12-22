using Godot;
using System.Collections.Generic;
using System;

namespace FinalEmblem.Core
{
    public partial class AnimationController : Node
    {
        private Game level;
        private ActionAnimator animator;

        public void Initialize(Game level)
        {
            this.level = level;
            level.OnTurnEnded += TurnEndedHandler;
        }

        public override void _ExitTree()
        {
            level.OnTurnEnded -= TurnEndedHandler;
        }

        public async void AnimateActionResults(Queue<ActionResult> results, Action onAnimationCompleted)
        {
            while (results.Count > 0)
            {
                var result = results.Dequeue();
                animator = InitializeAnimator(result);
                AddChild(animator);
                animator.StartAnimation();
                await ToSignal(animator, ActionAnimator.AnimCompleteSignal);
                animator.QueueFree();
            }

            await ToSignal(animator, "tree_exited");
            onAnimationCompleted.Invoke();
        }

        private static ActionAnimator InitializeAnimator(ActionResult action)
        {
            return action.result switch
            {
                ActionResultId.Moved => new MoveActionAnimator(action.actor, action.affected),
                ActionResultId.Waited => new WaitActionAnimator(action.actor),
                ActionResultId.Attacked => new AttackActionAnimator(action.actor, action.affected),
                ActionResultId.Collided => throw new NotImplementedException(),
                // ActionResultId.LostHp => throw new NotImplementedException(),
                ActionResultId.Died => new DeathActionAnimator(action.actor),
                _ => throw new NotImplementedException(),
            };
        }

        private void TurnEndedHandler()
        {
            for (int i = 0; i < level.Units.Count; i++)
            {
                level.Units[i].ToggleActedMaterial(false);
            }
        }
    }
}

