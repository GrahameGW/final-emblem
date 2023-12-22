using Godot;


namespace FinalEmblem.Core
{
    public partial class CurrentTurnDisplay : Control
    {
        private Label label;

        public override void _Ready()
        {
            label = GetChild<Label>(0);
        }

        public void SetFaction(Faction faction)
        {
            label.Text = $"{faction}'s Turn";
        }
    }
}

