using Godot;


namespace FinalEmblem.Core
{
    public partial class LevelHUD : CanvasLayer
    {
        private Game level;
        private GameMap map;
        private PlayerController player;

        private ActionList actionList;
        private CurrentTurnDisplay turnDisplay;
        private UnitInfoPanel unitInfoPanel;

        public void Initialize(Game level, GameMap map, PlayerController player)
        {
            this.level = level;
            this.map = map;
            this.player = player;

            level.OnTurnStarted += TurnStartedHandler;

            actionList = GetNode<ActionList>("ActionList");
            actionList.Initialize(player);

            turnDisplay = GetNode<CurrentTurnDisplay>("CurrentTurnDisplay");
            unitInfoPanel = GetNode<UnitInfoPanel>("UnitInfoPanel");
            unitInfoPanel.TogglePanel(null);

            var endTurnButton = GetNode<Button>("EndTurnButton");
            endTurnButton.Pressed += EndTurnButtonPressedHandler;

            map.OnSelectedTileChanged += SelectedTileChangedHandler;
        }

        public override void _ExitTree()
        {
            map.OnSelectedTileChanged -= SelectedTileChangedHandler;
        }

        private void EndTurnButtonPressedHandler()
        {
            level.EndTurn();
        }

        private void TurnStartedHandler(Faction faction)
        {
            actionList.TogglePlayersTurn(faction);
            turnDisplay.SetFaction(faction);
        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            unitInfoPanel.TogglePanel(tile?.Unit);
        }
    }
}

