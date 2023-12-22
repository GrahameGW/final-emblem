using Godot;

namespace FinalEmblem.Core
{
    public partial class AiController : ControllerBase
    {
        public override string DebugName => "AiController";

        private Game level;
        
        public void Initialize(Game level, Faction faction)
        {
            this.level = level;
            Faction = faction;
        }
        
        public override async void _EnterTree()
        {
            GD.Print("AI Turn");
            level.StartTurn(Faction);
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            ReleaseControl();
        }

        public override void _ExitTree()
        {
        }
    }
}

