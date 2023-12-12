using FinalEmblem.Core;
using System.Collections.Generic;

namespace FinalEmblem.QueryModel
{
    public class ActionExecutionTacticsState : TacticsState
    {
        private readonly IAction action;
        private readonly Unit actor;
        
        public ActionExecutionTacticsState(TacticsController context, IAction action, Unit actor) : base(context) 
        { 
            this.action = action;
            this.actor = actor;
        }

        public override void EnterState()
        {
            context.CanAcceptInput = false;
            // choose prior to calculations because it's player intent, not what actually happens
            if (action is MoveAction)
            {
                actor.HasMoved = true;
            }
            else
            {
                actor.HasActed = true;
            }
            
            var actuals = CombatService.CalculateActionImplications(actor, action);
            var results = new List<ActionResult>();
            for (int i = 0; i < actuals.Count; i++)
            {
                var res = actuals[i].Execute();
                results.Add(res);
            }

            var queue = new Queue<ActionResult>(results);
            context.Tokens.AnimateActionResults(queue, ActionAnimationCompletedHandler);
        }

        private void ActionAnimationCompletedHandler()
        {
            // if no other actions available end turn
            // else:
            context.CanAcceptInput = true;
            context.ChangeState(new IdleTacticsState(context));
            context.Map.SelectTile(actor.Tile);
        }
    }
}

