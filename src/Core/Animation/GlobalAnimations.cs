using Godot;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class GlobalAnimations : Node
    {
        [Export] PackedScene turnStartBanner;

        private Level level;

        public void Initialize(Level level)
        {
            this.level = level;
            level.OnTurnEnded += TurnEndedHandler;
        }

        public override void _ExitTree()
        {
            level.OnTurnEnded -= TurnEndedHandler;
        }

        private void TurnEndedHandler()
        {
            for (int i = 0; i < level.Units.Count; i++)
            {
                level.Units[i].ToggleActedMaterial(false);
            }
        }

        public async Task PlayTurnStartBanner(Faction turn)
        {
            var banner = turnStartBanner.Instantiate<TurnStartBanner>();
            banner.SetTurn(turn);
            await banner.Play(this);
        }
    }
}

