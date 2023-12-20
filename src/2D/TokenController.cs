using Godot;

using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System.Linq;
using System;

namespace FinalEmblem.Core
{
    public partial class TokenController : Node
    {
        public List<Unit> Units { get; private set; }

        public void Initialize()
        {
            Units = this.FindNodesOfType(new List<Unit>());
            for (int i = 0; i < Units.Count; i++)
            {
               // Tokens[i].GenerateUnitFromToken();
            }

            Level.OnTurnEnded += _ => TurnEndedHandler();
        }

        public override void _ExitTree()
        {
            Level.OnTurnEnded -= _ => TurnEndedHandler();
        }

        public async void AnimateActionResults(Queue<ActionResult> results, Action onAnimationCompleted)
        {
            while (results.Count > 0)
            {
                var result = results.Dequeue();
                var animator = InitializeAnimator(result);
                AddChild(animator);
                animator.StartAnimation();
                await ToSignal(animator, ActionAnimator.AnimCompleteSignal);
                animator.QueueFree();
            }

            onAnimationCompleted.Invoke();
        }

        private ActionAnimator InitializeAnimator(ActionResult action)
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
            for (int i = 0; i < Units.Count; i++)
            {
                Units[i].ToggleActedMaterial(false);
            }
        }
    }
}

