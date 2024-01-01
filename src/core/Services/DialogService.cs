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
            dialogic ??= new DialogicWrapper(node);
        }

        public static void Play(Action callback)
        {
            dialogic.Start("myfirsttimeline");
            void OnTimelineEnded()
            {
                callback.Invoke();
                dialogic.TimelineEnded -= OnTimelineEnded;
            }

            dialogic.TimelineEnded += OnTimelineEnded;
        }
        
        public static void PlayerLost(Action<bool> callback)
        {
            dialogic.Start("player_lost");
            void OnPlayerLost(bool res)
            {
                callback.Invoke(res);
                dialogic.SignalBoolEvent -= OnPlayerLost;
            }
            
            dialogic.SignalBoolEvent += callback;
        }
    }
}
