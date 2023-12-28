using Godot;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class TurnStartBanner : Control
    {
        [Export] Label label;
        [Export] Color playerColor;
        [Export] Color enemyColor;
        [Export] Color otherColor;
        [Export] AnimationPlayer player;


        public void SetTurn(Faction faction)
        {
            if (faction == Faction.Player)
            {
                SetLabel(playerColor, "Player");
            }
            else if (faction == Faction.Enemy)
            {
                SetLabel(enemyColor, "Enemy");
            }
            else
            {
                SetLabel(otherColor, "Other");
            }
        }

        private void SetLabel(Color color, string faction)
        {
            label.Text = $"{faction} Turn";
            label.AddThemeColorOverride(MagicString.FONT_COLOR, color);
        }

        public async Task Play(Node parent)
        {
            parent.AddChild(this);
            await ToSignal(player, MagicString.ANIM_FINISHED);
            QueueFree();
        }
    }
}

