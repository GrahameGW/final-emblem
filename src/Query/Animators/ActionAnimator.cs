using Godot;

namespace FinalEmblem.QueryModel
{
    public abstract partial class ActionAnimator : Node
    {
        [Signal]
        public delegate void AnimationCompleteEventHandler();
        public static StringName AnimCompleteSignal => new("AnimationComplete");

        public abstract void StartAnimation();
    }
}

