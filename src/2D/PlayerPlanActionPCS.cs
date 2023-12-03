using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class PlayerPlanActionPCS : PlayerControlState
    {
        private readonly IActionPlanner planner;

        public PlayerPlanActionPCS(PlayerController player, IActionPlanner planner) : base(player)
        {
            this.planner = planner;
        }

        public override void EnterState()
        {
            planner.BuildAction += ActionBuiltHandler;
        }

        public override void ExitState()
        {
            planner.BuildAction -= ActionBuiltHandler;
            player.ActiveActionPlanner.QueueFree();
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
            planner.HandleInput(input);
        }

        private void ActionBuiltHandler(IAction action)
        {
            player.ChangeState(new PlayerActionExecutingPCS(action, player));
        }
    }
}

