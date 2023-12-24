using Godot;

namespace FinalEmblem.Core
{
    public abstract partial class ActionAnimator : Node
    {
        [Signal]
        public delegate void AnimationCompleteEventHandler();
        public static StringName AnimCompleteSignal => new("AnimationComplete");
    }
}

