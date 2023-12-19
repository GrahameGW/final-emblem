﻿using Godot;
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

            Level.OnTurnEnded += _ => TurnEndedHandler();
        }

        public override void _ExitTree()
        {
            Level.OnTurnEnded -= _ => TurnEndedHandler();
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
                animator.StartAnimation();
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
                ActionResultId.Attacked => new AttackActionAnimator(actor, action.affected),
                ActionResultId.Collided => throw new NotImplementedException(),
                // ActionResultId.LostHp => throw new NotImplementedException(),
                ActionResultId.Died => new DeathActionAnimator(actor),
                _ => throw new NotImplementedException(),
            };
        }

        private void TurnEndedHandler()
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                Tokens[i].ToggleActedMaterial(false);
            }
        }
    }
}
