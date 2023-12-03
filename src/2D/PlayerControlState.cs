using Godot;

namespace FinalEmblem.Godot2D
{
    public abstract partial class PlayerControlState : GodotObject
    {
        protected PlayerController player;

        public abstract void EnterState();
        public abstract void HandleInput(InputEvent input);
        public abstract void ExitState();

        public PlayerControlState(PlayerController player)
        {
            this.player = player;
        }
    }


}

