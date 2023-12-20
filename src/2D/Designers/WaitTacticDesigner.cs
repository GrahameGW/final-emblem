using Godot;
using System;

namespace FinalEmblem.Core
{
    public partial class WaitTacticDesigner : Node, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        private readonly Unit unit;

        public WaitTacticDesigner(Unit unit) 
        {
            this.unit = unit;
        }

        public override void _EnterTree()
        {
            OnActionBuilt?.Invoke(new WaitAction { Actor = unit });
            QueueFree();
        }
    }
}

