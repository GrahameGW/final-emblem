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
            context.ChangeState(new IdleTacticsState(context));

            if (context.Level.HaveAllUnitsActed())
            {
                context.Level.NextTurn();
            }
            else
            {
                context.Map.SelectTile(actor.Tile);
            }
        }
    }
}

