using Godot;

namespace FinalEmblem.Core
{
    public abstract class PlayerState 
    {
        protected PlayerController _context;

        public virtual void EnterState(PlayerController context) { }
        public virtual void ExitState() { }
        public virtual void Update(float delta) { }
        public virtual void HandleInput(InputEvent input) { }
    }
}

