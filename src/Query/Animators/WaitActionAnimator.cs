using Godot;

namespace FinalEmblem.QueryModel
{
    public partial class WaitActionAnimator : ActionAnimator
    {
        private UnitToken token;

        public WaitActionAnimator(UnitToken token) 
        {
            this.token = token;
        }

        public override async void StartAnimation()
        {
            token.ToggleActedMaterial(true);
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

