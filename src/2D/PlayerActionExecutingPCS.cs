using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerActionExecutingPCS : PlayerControlState
    {
        private readonly IAction action;
        
        public PlayerActionExecutingPCS(IAction action, PlayerController player) : base(player)
        {
            this.action = action;
        }

        public override void EnterState()
        {
            GD.Print("Entered PlayerActionExecuting");
            action.Execute(player.SelectedTile.Unit);
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerActionExecuting");
            player.ChangeState(new PlayerTurnIdlePCS(player));
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
        }
    }
}

