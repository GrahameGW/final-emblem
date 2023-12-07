using FinalEmblem.Core;
using Godot;

namespace FinalEmblem.QueryModel
{
    public partial class PlayerActionExecutingPCS : TacticsState
    {
        private readonly UnitToken unitNode;
        private TacticsState queuedState;

        public PlayerActionExecutingPCS(UnitToken node, TacticsController player) : base(player)
        {
            unitNode = node;
        }

        public override void EnterState()
        {
            GD.Print("Entered PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted += ActionPlaybackCompletedHandler;
            Level.OnTurnEnded += TurnEndedHandler;
            context.Map.ClearTileHighlights();
            unitNode.PlayNextAction();
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted -= ActionPlaybackCompletedHandler;
            Level.OnTurnEnded -= TurnEndedHandler;
        }

        public override void HandleInput(InputEvent input)
        {
        }

        private void ActionPlaybackCompletedHandler()
        {
            /*
            player.SelectedTile = unitNode.Unit.Tile;
            if (queuedState != null)
            {
                player.Level.StartNextTurn();
                player.ChangeState(queuedState);
            }
            else
            {
                player.ChangeState(new IdleTacticsState(player));
            }
            */
        }

        private void TurnEndedHandler(Faction faction)
        {
            if (faction == Faction.Player)
            {
                //queuedState = new OtherTurnPCS(player);
            }
        }
    }
}

