using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerActionExecutingPCS : PlayerControlState
    {
        private readonly UnitNode unitNode;
        
        public PlayerActionExecutingPCS(UnitNode node, PlayerController player) : base(player)
        {
            unitNode = node;
        }

        public override void EnterState()
        {
            GD.Print("Entered PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted += ActionPlaybackCompletedHandler;
            player.Map.ClearTileHighlights();
            unitNode.PlayNextAction();
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerActionExecuting");
            unitNode.OnActionPlaybackCompleted -= ActionPlaybackCompletedHandler;
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
        }

        private void ActionPlaybackCompletedHandler()
        {
            player.ChangeState(new PlayerTurnIdlePCS(player));
        }
    }
}

