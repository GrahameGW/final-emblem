namespace FinalEmblem.Core
{
    public partial class NoOpAnimation : OneShotAnimation
    {
        public override void _EnterTree()
        {
            EmitSignal(AnimCompleteSignal);
        }
    }
}

