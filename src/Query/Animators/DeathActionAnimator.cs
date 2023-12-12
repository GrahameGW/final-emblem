using Godot;

namespace FinalEmblem.QueryModel
{
    public partial class DeathActionAnimator : ActionAnimator
    {
        private UnitToken actor;

        public DeathActionAnimator(UnitToken token)
        {
            actor = token;
        }

        public override async void StartAnimation()
        {
            actor.QueueFree();
            await ToSignal(actor, Node.SignalName.TreeExited);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

