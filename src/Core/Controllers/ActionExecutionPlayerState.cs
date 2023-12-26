using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class ActionExecutionPlayerState : PlayerState
    {
        private readonly IAction action;
        private readonly Unit actor;
        
        public ActionExecutionPlayerState(IAction action, Unit actor) 
        {
            this.action = action;
            this.actor = actor;
        }

        public override void EnterState(PlayerController context)
        {
            _context = context;

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
            _context.Level.ExecuteActionQueue(actuals, ActionCompletedHandler);
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

