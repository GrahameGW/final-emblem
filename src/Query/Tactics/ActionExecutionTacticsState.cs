using FinalEmblem.Core;

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
            var actuals = CombatService.CalculateActionImplications(action);
            for (int i = 0; i < actuals.Count; i++)
            {
                actuals[i].Execute(actor);
            }

            context.ChangeState(new ActionPlaybackTacticsState(context, actuals));
        }
    }
}

