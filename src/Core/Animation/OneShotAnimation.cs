using Godot;

namespace FinalEmblem.Core
{
    public abstract partial class OneShotAnimation : Node
    {
        [Signal]
        public delegate void AnimationCompleteEventHandler();
        public static StringName AnimCompleteSignal => new("AnimationComplete");
    }
}

