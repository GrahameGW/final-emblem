using Godot;
using System;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public partial class WaitTacticDesigner : Node, ITacticDesigner
    {
        public Action<List<IUnitAction>> OnActionBuilt { get; set; }

        private readonly Unit unit;

        public WaitTacticDesigner(Unit unit) 
        {
            this.unit = unit;
        }

        public override void _EnterTree()
        {
            var actuals = CombatService.CalculateWaitImplications(unit);
            OnActionBuilt?.Invoke(actuals);
            QueueFree();
        }
    }
}

