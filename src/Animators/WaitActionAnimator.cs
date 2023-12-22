using Godot;

namespace FinalEmblem.Core
{
    public partial class WaitActionAnimator : ActionAnimator
    {
        private Unit actor;

        public WaitActionAnimator(Unit unit) 
        {
            actor = unit;
        }

        public override async void StartAnimation()
        {
            actor.ToggleActedMaterial(true);
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

