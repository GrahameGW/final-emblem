using Godot;
using FinalEmblem.Core;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Godot2D
{
    public partial class MoveActionPlayback : ActionPlayback
    {
        private Vector2 start;
        private Vector2 destination;
        private float speed;
        private float distance;
        private float progress;

        private const float ROOT_TWO = 1.41421356f;

        public MoveActionPlayback(UnitNode unit, Tile from, Tile to, float speed) : base(unit) 
        {
            start = from.WorldPosition.Vector2XY();
            destination = to.WorldPosition.Vector2XY();
            distance = from.IsDiagonalTo(to) ? ROOT_TWO : 1;
            this.speed = speed / distance;
        
        }

        public override void Update(double delta)
        {
            progress += speed * (float)delta;
            unit.Position = start.Lerp(destination, progress);

            if (progress >= 1)
            {
                unit.Position = destination;
                unit.PlayNextAction();
            }
        }
    }
}

