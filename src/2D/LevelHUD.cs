using Godot;


namespace FinalEmblem.Core
{
    public partial class LevelHUD : CanvasLayer
    {
        private Level level;
        private GameMap map;
        private TacticsController tactics;

        private ActionList actionList;
        private CurrentTurnDisplay turnDisplay;

        public void Initialize(Level level, GameMap map, TacticsController tactics)
        {
            this.level = level;
            this.map = map;
            this.tactics = tactics;

            level.OnTurnStarted += TurnStartedHandler;

            actionList = GetNode<ActionList>("ActionList");
            actionList.Initialize(tactics);

            turnDisplay = GetNode<CurrentTurnDisplay>("CurrentTurnDisplay");

            var endTurnButton = GetNode<Button>("EndTurnButton");
            endTurnButton.Pressed += EndTurnButtonPressedHandler;
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
    }
}

