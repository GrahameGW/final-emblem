using Godot;

namespace FinalEmblem.Core
{
    public partial class MidTurnController : ControllerBase
    {
        public override string DebugName => "MidTurnController";
        
        public override async void _EnterTree()
        {
            GD.Print("In between turns!");
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            ReleaseControl();
        }

        public override void _ExitTree()
        {

        }
    }
}

