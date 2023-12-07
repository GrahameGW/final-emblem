using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.QueryModel
{
    public class ActionDesignTacticsState : TacticsState
    {
        private readonly Unit unit;
        private readonly ITacticDesigner designer;

        public ActionDesignTacticsState(TacticsController player, ITacticDesigner designer, Unit unit) : base(player)
        {
            this.designer = designer;
            this.unit = unit;
        }

        public override void EnterState()
        {
            designer.OnActionBuilt += ActionBuiltHandler;
            if (designer is Node node)
            {
                context.AddChild(node);
            }
        }

        public override void ExitState()
        {
            designer.OnActionBuilt -= ActionBuiltHandler;
            if (designer is Node node)
            {
                node.QueueFree();
            }
        }

        public override void HandleInput(InputEvent input)
        {
            if (Input.IsActionPressed(InputAction.CANCEL))
            {
                context.ChangeState(new IdleTacticsState(context));
                context.Map.SetSelectedTile(unit.Tile);
            }
        }

        public override void SetTileUnderMouse(Tile tile)
        {
            designer.SetTileUnderMouse(tile);
        }

        public override void SetSelectedTile(Tile tile)
        {
            designer.SetSelectedTile(tile);
        }

        private void ActionBuiltHandler(IAction action)
        {
            context.ChangeState(new ActionExecutionTacticsState(context, action, unit));
        }
    }
}

