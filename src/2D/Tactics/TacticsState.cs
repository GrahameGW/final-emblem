using FinalEmblem.Core;
using Godot;

namespace FinalEmblem.QueryModel
{
    public abstract class TacticsState : IStateFSM
    {       
        protected TacticsController context;
        protected Tile tileUnderMouse;
        protected Tile selectedTile;

        public virtual void EnterState() {}
        public virtual void HandleInput(InputEvent input) {}
        public virtual void ExitState() {}

        public virtual void SetTileUnderMouse(Tile tile) { }
        public virtual void SetSelectedTile(Tile tile) { }

        public TacticsState(TacticsController context)
        {
            this.context = context;
        }
    }

    public interface IStateFSM
    {
        void EnterState() { }
        void HandleInput(InputEvent input) { }
        void ExitState() { }
    }
}

