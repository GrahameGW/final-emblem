using Godot;
using FinalEmblem.Core;


namespace FinalEmblem.Godot2D
{
    public partial class PlayerTurnIdlePCS : PlayerControlState
    {
        public PlayerTurnIdlePCS(PlayerController player) : base(player) { }

        public override void EnterState()
        {
            GD.Print("Entered PlayerTurnIdle");
            player.SelectedTile = null;
            player.IsActing = true;
            player.OnActionPlanningStarted += ActionPlanningHandler;
            Level.OnTurnStarted += FactionTurnStartHandler;
        }

        public override void ExitState()
        {
            GD.Print("Exited PlayerTurnIdle");
            player.IsActing = false;
            player.OnActionPlanningStarted -= ActionPlanningHandler;
            Level.OnTurnStarted -= FactionTurnStartHandler;
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
            if (input is InputEventMouseButton && input.IsPressed())
            {
                var tile = player.GetTileUnderMouse();
                player.SelectedTile = tile;

                if (tile.Unit != null && !tile.Unit.HasMoved)
                {
                    var inRange = NavService.FindTilesInRange(tile.Unit.Move, tile, includeStart: false);
                    player.Map.HighlightGameTiles(inRange);
                }
                else
                {
                    player.Map.ClearTileHighlights();
                }
            }
        }

        private void FactionTurnStartHandler(Faction faction)
        {
            if (faction != Faction.Player)
            {
                player.ChangeState(new OtherTurnPCS(player));
            }
        }

        private void ActionPlanningHandler(IActionPlanner planner)
        {
            player.ChangeState(new PlayerPlanActionPCS(player, planner));
        }
    }

}

