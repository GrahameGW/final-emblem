using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class ActionExecutionPlayerState : PlayerState
    {
        private readonly List<IUnitAction> actions;
        private readonly Unit actor;
        
        public ActionExecutionPlayerState(List<IUnitAction> actions, Unit actor) 
        {
            this.actions = actions;
            this.actor = actor;
        }

        public override void EnterState(PlayerController context)
        {
            _context = context;
            _context.Level.ExecuteActionQueue(actions, ActionCompletedHandler);
        }

        private void ActionCompletedHandler()
        {
            _context.ChangeState(new InitialPlayerState(_context.SelectedTile));
        }

        public override void ExitState()
        {
            _context.Level.TestWinConditions();
            if (_context.Level.HaveAllUnitsActed())
            {
                _context.Level.EndTurn();
            }
        }
    }
}

