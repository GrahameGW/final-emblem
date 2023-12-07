using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.QueryModel
{
    public partial class LevelHUD : CanvasLayer
    {
        private QueryLevel level;
        private GameMap map;
        private TacticsController tactics;

        private ActionList actionList;
        private CurrentTurnDisplay turnDisplay;

        public void Initialize(QueryLevel level, GameMap map, TacticsController tactics)
        {
            this.level = level;
            this.map = map;
            this.tactics = tactics;

            map.OnSelectedTileChanged += SelectedTileChangedHandler;
            level.OnTurnStarted += TurnStartedHandler;

            actionList = GetNode<ActionList>("ActionList");
            actionList.Initialize(tactics);

            turnDisplay = GetNode<CurrentTurnDisplay>("CurrentTurnDisplay");

            var endTurnButton = GetNode<Button>("EndTurnButton");
            endTurnButton.Pressed += EndTurnButtonPressedHandler;
        }

        public override void _ExitTree()
        {
            map.OnSelectedTileChanged -= SelectedTileChangedHandler;
        }

        private void EndTurnButtonPressedHandler()
        {
            level.NextTurn();
        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            actionList.UpdatePanelForTile(tile);
        }

        private void TurnStartedHandler(Faction faction)
        {
            actionList.TogglePlayersTurn(faction);
            turnDisplay.SetFaction(faction);
        }
    }
}

