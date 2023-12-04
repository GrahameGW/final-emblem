using Godot;
using FinalEmblem.Core;
using System.Linq;
using System.Collections.Generic;

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
            GD.Print("Entered PlayerPlanAction");
            planner.OnActionsBuilt += ActionsBuiltHandler;
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerPlanAction");
            planner.OnActionsBuilt -= ActionsBuiltHandler;
            player.ActiveActionPlanner.QueueFree();
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
            var mouse = input as InputEventMouseButton;
            if (mouse?.ButtonIndex == MouseButton.Right && mouse.IsPressed())
            {
                player.ChangeState(new PlayerTurnIdlePCS(player));
            }
            else
            {
                planner.HandleInput(input);
            }
        }

        private void ActionsBuiltHandler(List<IAction> actions)
        {
            var node = player.UnitGroup.UnitNodes.First(u => u.Unit == player.SelectedTile.Unit);
            foreach ( var action in actions )
            {
                node.Unit.EnqueueAction(action);
            }
            player.ChangeState(new PlayerActionExecutingPCS(node, player));
        }
    }
}

