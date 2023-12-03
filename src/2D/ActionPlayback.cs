using Godot;

namespace FinalEmblem.Godot2D
{
    public abstract partial class ActionPlayback : GodotObject
    {
        protected UnitNode unit;

        public ActionPlayback(UnitNode unit) 
        {
            this.unit = unit;
        }

        public abstract void Update(double delta);
    }
}

