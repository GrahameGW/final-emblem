using Godot;

namespace FinalEmblem.Core
{
    public class ActionDesignPlayerState : PlayerState
    {
        private readonly Unit unit;
        private readonly ITacticDesigner designer;

        public ActionDesignPlayerState(ITacticDesigner designer, Unit unit)
        {
            this.designer = designer;
            this.unit = unit;
        }

        public override void EnterState(PlayerController context)
        {
            _context = context;
            designer.OnActionBuilt += ActionBuiltHandler;
            _context.Map.OnTileUnderMouseChanged += TileUnderMouseChangedHandler;
            _context.Map.OnSelectedTileChanged += SelectedTileChangedHandler;
            if (designer is Node node)
            {
                _context.AddChild(node);
            }
        }

        public override void ExitState()
        {
            designer.OnActionBuilt -= ActionBuiltHandler;
            _context.Map.OnTileUnderMouseChanged -= TileUnderMouseChangedHandler;
            _context.Map.OnSelectedTileChanged -= SelectedTileChangedHandler;
            if (designer is Node node)
            {
                node.QueueFree();
            }
        }

        public override void HandleInput(InputEvent input)
        {
            if (Input.IsActionPressed(InputAction.CANCEL))
            {
                _context.ChangeState(new InitialPlayerState(unit.Tile));
            }
        }

        private void ActionBuiltHandler(IAction action)
        {
            _context.ChangeState(new ActionExecutionPlayerState(action, unit));
        }

        private void TileUnderMouseChangedHandler(Tile tile)
        {
            designer.SetTileUnderMouse(tile);

        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            designer.SetSelectedTile(tile);
            _context.SelectedTile = tile;
        }
    }
}

