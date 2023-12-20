namespace FinalEmblem.Core
{
    public partial class AiController : ControllerBase
    {
        public AiController(Level level, Faction faction) : base(level) 
        {
            Faction = faction;
        }
        
        public override void _EnterTree()
        {
            level.StartTurn(Faction);
        }

        public override void _ExitTree()
        {
        }
    }
}

