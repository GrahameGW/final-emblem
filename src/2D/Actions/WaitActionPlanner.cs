using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using System;

namespace FinalEmblem.Godot2D
{
    public partial class WaitActionPlanner : Node, IActionPlanner
    {
        public Action<List<IAction>> OnActionsBuilt { get; set; }

        public override void _Ready()
        {
            Wait();
        }

        public void HandleInput(InputEvent input)
        {

        }

        private async void Wait()
        {
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            var wait = new WaitAction();
            OnActionsBuilt?.Invoke(new List<IAction> { wait });
        }
    }
}

