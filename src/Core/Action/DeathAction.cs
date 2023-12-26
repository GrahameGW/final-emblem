using System;

namespace FinalEmblem.Core
{
    public class DeathAction : IAction
    {
        public ActionType Type => ActionType.Die;
        public Unit Actor { get; set; }
        public Action<Unit> OnDeathCallback { get; set; }
        
        public ActionResult Execute()
        {
            OnDeathCallback?.Invoke(Actor);
            return new ActionResult(Actor, ActionResultId.Died);
        }
    }
}

