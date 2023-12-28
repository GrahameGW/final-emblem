using Godot;

namespace FinalEmblem.Core
{
    public partial class MidTurnController : ControllerBase
    {
        public override string DebugName => "MidTurnController";

        private Game level;
        private AnimationController animator;

        public void Initialize(Game game, AnimationController animController)
        {
            level = game;
            animator = animController;
        }

        public override async void _EnterTree()
        {
            var faction = level.NextFaction;
            await animator.PlayTurnStartBanner(faction);
            ReleaseControl();
        }
    }
}

