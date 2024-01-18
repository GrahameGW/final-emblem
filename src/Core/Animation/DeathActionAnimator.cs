using Godot;

namespace FinalEmblem.Core
{
    public partial class DeathActionAnimator : OneShotAnimation
    {
        private Unit unit;
        private AnimationPlayer animator;

        public DeathActionAnimator(Unit unit)
        {
            this.unit = unit;
            animator = unit.GetNode<AnimationPlayer>("AnimationPlayer");
        }

        public override async void _EnterTree()
        {
            animator.Play("death");
            await ToSignal(animator, MagicString.ANIM_FINISHED);
            unit.QueueFree();
            EmitSignal(AnimCompleteSignal);
        }
    }
}

