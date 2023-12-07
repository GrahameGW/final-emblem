using Godot;
using FinalEmblem.Core;
using System;

namespace FinalEmblem.QueryModel
{
    public partial class WaitTacticDesigner : Node, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        public override void _EnterTree()
        {
            OnActionBuilt?.Invoke(new WaitAction());
            QueueFree();
        }
    }
}

