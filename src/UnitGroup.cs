using System;
using System.Collections.Generic;
using Godot;
using FinalEmblem.Core;

namespace FinalEmblem.Godot2D
{
    public partial class UnitGroup : Node
    {
        public static Action<UnitGroup> OnUnitActionsCompleted;
        public readonly List<Unit> Units = new();

        public void StartTurn()
        {

        }
    }
}
