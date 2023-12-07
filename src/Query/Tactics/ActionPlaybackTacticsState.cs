using FinalEmblem.Core;
using System.Collections.Generic;

namespace FinalEmblem.QueryModel
{
    public class ActionPlaybackTacticsState : TacticsState
    {
        List<IAction> actions;
        
        public ActionPlaybackTacticsState(TacticsController context, List<IAction> actions) : base(context)
        {
            this.actions = actions;
        }

        public override void EnterState()
        {
            context.ChangeState(new IdleTacticsState(context));
        }
    }
}

