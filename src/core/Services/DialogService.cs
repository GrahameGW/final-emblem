using Godot;
using System;
using System.Threading.Tasks;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public static class DialogService
    {
        private static DialogicWrapper dialogic;
        
        public static void Initialize(Node node)
        {
            dialogic = new DialogicWrapper(node);
        }

        public static void Play(Action callback)
        {
            dialogic.Start("myfirsttimeline");
            dialogic.TimelineEnded += callback;
        }

        public static void PlayerLost(Action<bool> callback)
        {
            dialogic.Start("player_lost");
            dialogic.SignalBoolEvent += callback;
        }
    }
}
