using Godot;
using System;

namespace FinalEmblem.Core
{
    public abstract partial class ControllerBase : Node
    {
        public Faction Faction { get; protected set; }

        public event Action OnControllerExited;

        protected Level level;

        public ControllerBase(Level level) 
        {
            this.level = level;
        }

        protected void ReleaseControl()
        {
            GetParent().RemoveChild(this);
            OnControllerExited?.Invoke();
        }
    }
}

