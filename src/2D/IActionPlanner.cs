using Godot;
using FinalEmblem.Core;
using System;

namespace FinalEmblem.Godot2D
{
    public interface IActionPlanner
    {
        void HandleInput(InputEvent input);
        Action<IAction> BuildAction { get; set; }
    }
}

