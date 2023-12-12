namespace FinalEmblem.QueryModel
{
    public partial class WaitActionAnimator : ActionAnimator
    {
        private UnitToken token;

        public WaitActionAnimator(UnitToken token) 
        {
            this.token = token;
        }

        public override void _EnterTree()
        {
            token.ToggleActedMaterial();
            EmitSignal(AnimCompleteSignal);
        }
    }
}

