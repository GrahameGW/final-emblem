using Godot;
using FinalEmblem.Core;
using System;
using System.Collections.Generic;

namespace FinalEmblem.Godot2D
{
    public interface IActionPlanner
    {
        void HandleInput(InputEvent input);
        Action<List<IAction>> OnActionsBuilt { get; set; }
    }
}

