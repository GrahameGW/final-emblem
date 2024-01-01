
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class MidTurnController : ControllerBase
    {
        public override string DebugName => "MidTurnController";

        private Level level;
        private GlobalAnimations animator;

        public void Initialize(Level game, GlobalAnimations animController)
        {
            level = game;
            animator = animController;
        }

        public override async void _EnterTree()
        {
            if (level.Round == 0)
            {
                DialogService.Play(async () => await TurnBanner());
            }
            else
            {
                await TurnBanner();
            }
        }

        private async Task TurnBanner()
        {
            var faction = level.NextFaction;
            await animator.PlayTurnStartBanner(faction);
            ReleaseControl();
        }
    }
}

