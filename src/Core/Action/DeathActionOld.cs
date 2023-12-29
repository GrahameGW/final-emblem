using System;

namespace FinalEmblem.Core
{
    public class DeathActionOld : IAction
    {
        public ActionType Type => ActionType.Die;
        public Unit Actor { get; set; }
        public Action<Unit> OnDeathCallback { get; set; }
        
        public IActionResult Execute()
        {
            OnDeathCallback?.Invoke(Actor);
            return new BaseActionResult(Actor);
        }
    }
}

