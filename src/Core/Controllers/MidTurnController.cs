﻿namespace FinalEmblem.Core
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
            var faction = level.NextFaction;
            await animator.PlayTurnStartBanner(faction);
            ReleaseControl();
        }
    }
}

