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
            planner.OnActionsBuilt += ActionsBuiltHandler;
        }

        public override void ExitState()
        {
            planner.OnActionsBuilt -= ActionsBuiltHandler;
            player.ActiveActionPlanner.QueueFree();
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
            planner.HandleInput(input);
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

