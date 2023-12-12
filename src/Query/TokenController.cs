using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System.Linq;
using System;

namespace FinalEmblem.QueryModel
{
    public partial class TokenController : Node
    {
        public List<UnitToken> Tokens { get; private set; }
        public List<Unit> Units
        {
            get => Tokens.Select(t => t.Unit).ToList();
        }

        public void Initialize()
        {
            Tokens = this.FindNodesOfType(new List<UnitToken>());
            for (int i = 0; i < Tokens.Count; i++)
            {
                Tokens[i].GenerateUnitFromToken();
            }
        }

        public UnitToken GetTokenFromUnit(Unit unit)
        {
            return Tokens.FirstOrDefault(t => t.Unit == unit);
        }

        public async void AnimateActionResults(Queue<ActionResult> results, Action onAnimationCompleted)
        {
            while (results.Count > 0)
            {
                var result = results.Dequeue();
                var animator = InitializeAnimator(result);
                AddChild(animator);
                await ToSignal(animator, ActionAnimator.AnimCompleteSignal);
                animator.QueueFree();
            }

            onAnimationCompleted.Invoke();
        }

        private ActionAnimator InitializeAnimator(ActionResult action)
        {
            var actor = GetTokenFromUnit(action.actor);
            return action.result switch
            {
                ActionResultId.Moved => new MoveActionAnimator(actor, action.affected),
                ActionResultId.Waited => new WaitActionAnimator(actor),
                ActionResultId.Attacked => throw new NotImplementedException(),
                ActionResultId.Collided => throw new NotImplementedException(),
                ActionResultId.LostHp => throw new NotImplementedException(),
                ActionResultId.Died => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}

