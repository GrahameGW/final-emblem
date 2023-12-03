using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class CurrentTurnDisplay : Control
    {
        private Label label;

        public void Initialize()
        {
            label = GetChild<Label>(0);
            Level.OnTurnStarted += TurnStartedHandler;
        }

        public override void _ExitTree()
        {
            Level.OnTurnStarted -= TurnStartedHandler;
        }

        private void TurnStartedHandler(Faction faction)
        {
            label.Text = $"{faction}'s Turn";
        }
    }
}

