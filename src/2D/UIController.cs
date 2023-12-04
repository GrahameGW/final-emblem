using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class UIController : CanvasLayer
    {
        private Level level;

        public void Initialize(Level level, PlayerController player)
        {
            this.level = level;

            var actionList = GetNode<ActionList>("ActionList");
            actionList.Initialize(player);

            var endTurnButton = GetNode<Button>("EndTurnButton");
            endTurnButton.Pressed += EndTurnButtonPressedHandler;

            var currentTurnDisplay = GetNode<CurrentTurnDisplay>("CurrentTurnDisplay");
            currentTurnDisplay.Initialize();
        }

        private void EndTurnButtonPressedHandler()
        {
            level.EndTurn();
            level.StartNextTurn();
        }
    }
}

