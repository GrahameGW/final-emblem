using Godot;

namespace FinalEmblem.Core
{
    public partial class SpeakActionAnimation : OneShotAnimation
    {
        private Unit player;
        private Resource timeline;

        public SpeakActionAnimation(SpeakAction action) 
        {
            player = action.Player;
            timeline = action.Timeline;
        }

        public override void _EnterTree()
        {
            DialogService.Play(timeline, OnConversationEndedHandler);
        }

        private void OnConversationEndedHandler()
        {
            EmitSignal(AnimCompleteSignal);
        }
    }
}

