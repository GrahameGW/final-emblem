namespace FinalEmblem.Godot2D
{
    public partial class WaitActionPlayback : ActionPlayback
    {
        public WaitActionPlayback(UnitNode unit) : base(unit) { }

        public override void Update(double delta)
        {
            unit.PlayNextAction();
        }
    }
}

