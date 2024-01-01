using Godot;
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

        public static async Task Play()
        {
            dialogic.Start("myfirsttimeline");
            await AsyncEvent.AwaitAction(dialogic.TimelineEnded);
        }
    }
}
