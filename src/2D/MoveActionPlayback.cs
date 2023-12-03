using Godot;
using FinalEmblem.Core;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Godot2D
{
    public partial class MoveActionPlayback : ActionPlayback
    {
        private Vector2 destination;
        private float speed;
        private Vector2 path;

        public MoveActionPlayback(UnitNode unit, Tile from, Tile to, float speed) : base(unit) 
        {
            destination = to.WorldPosition.Vector2XY();
            this.speed = speed;
            path = (to.WorldPosition - from.WorldPosition).Vector2XY();
        
        }

        public override void Update(double delta)
        {
            unit.Position += (float)delta * speed * path;

            if (unit.Position.DistanceTo(destination) < 0.1f)
            {
                unit.Position = destination;
                unit.PlayNextAction();
            }
        }
    }
}

