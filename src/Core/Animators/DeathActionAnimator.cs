using Godot;

namespace FinalEmblem.Core
{
    public partial class DeathActionAnimator : ActionAnimator
    {
        private Unit actor;

        public DeathActionAnimator(Unit unit)
        {
            actor = unit;
        }

        public override async void _EnterTree()
        {
            actor.QueueFree();
            await ToSignal(actor, Node.SignalName.TreeExited);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

