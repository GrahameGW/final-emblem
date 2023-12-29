using Godot;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class ActionRunner : Node
    {
        private IUnitAction action;
        private OneShotAnimation animation;

        public ActionRunner(IUnitAction action, OneShotAnimation anim)
        {
            this.action = action;
            animation = anim;
        }

        public async Task Run()
        {
            AddChild(animation);
            await ToSignal(animation, OneShotAnimation.AnimCompleteSignal);
            action.Execute();
            animation.QueueFree();
        }
    }
}

