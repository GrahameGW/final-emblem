using Godot;
using System;

namespace FinalEmblem.Core
{
    public abstract partial class ControllerBase : Node
    {
        public Faction Faction { get; protected set; }
        public abstract string DebugName { get; }

        public event Action OnControllerExited;

        protected void ReleaseControl()
        {
            GetParent().CallDeferred("remove_child", this);
            OnControllerExited?.Invoke();
        }
    }
}

