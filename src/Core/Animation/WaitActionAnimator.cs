﻿using Godot;

namespace FinalEmblem.Core
{
    public partial class WaitActionAnimator : OneShotAnimation
    {
        private Unit actor;

        public WaitActionAnimator(Unit unit) 
        {
            actor = unit;
        }

        public override async void _EnterTree()
        {
            actor.ToggleActedMaterial(true);
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

