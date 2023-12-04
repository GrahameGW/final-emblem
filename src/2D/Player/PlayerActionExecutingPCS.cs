using FinalEmblem.Core;
using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerActionExecutingPCS : PlayerControlState
    {
        private readonly UnitNode unitNode;
        private PlayerControlState queuedState;

        public PlayerActionExecutingPCS(UnitNode node, PlayerController player) : base(player)
        {
            unitNode = node;
        }

        public override void EnterState()
        {
            GD.Print("Entered PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted += ActionPlaybackCompletedHandler;
            Level.OnTurnEnded += TurnEndedHandler;
            player.Map.ClearTileHighlights();
            unitNode.PlayNextAction();
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted -= ActionPlaybackCompletedHandler;
            Level.OnTurnEnded -= TurnEndedHandler;
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
        }

        private void ActionPlaybackCompletedHandler()
        {
            player.SelectedTile = unitNode.Unit.Tile;
            if (queuedState != null)
            {
                player.Level.StartNextTurn();
                player.ChangeState(queuedState);
            }
            else
            {
                player.ChangeState(new PlayerTurnIdlePCS(player));
            }
            
        }

        private void TurnEndedHandler(Faction faction)
        {
            if (faction == Faction.Player)
            {
                queuedState = new OtherTurnPCS(player);
            }
        }
    }
}

