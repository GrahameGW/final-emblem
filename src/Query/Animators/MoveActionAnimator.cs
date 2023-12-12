using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using Godot;

namespace FinalEmblem.QueryModel
{
    public partial class MoveActionAnimator : ActionAnimator
    {
        private UnitToken token;
        private List<Tile> path;
        private float speed;
        private float stepSize;
        private int posIndex;
        private float progress;
        private Vector2 start;
        private Vector2 end;

        private const float ROOT_TWO = 1.41421356f;

        public MoveActionAnimator(UnitToken token, List<Tile> path)
        {
            this.path = path;
            this.token = token;
            speed = token.TravelSpeed;
            progress = 0f;
            posIndex = 0;
        }

        public override void _EnterTree()
        {
            if (path.Count < 2)
            {
                EmitSignal(AnimCompleteSignal);
                return;
            }

            NextSegment(posIndex);
        }

        public override void _Process(double deltaTime)
        {
            progress += stepSize * (float)deltaTime;
            token.Position = start.Lerp(end, progress);

            if (progress >= 1)
            {
                token.Position = end;
                posIndex += 1;
                if (posIndex == path.Count - 1)
                {
                    EmitSignal(AnimCompleteSignal);
                }
                else
                {
                    NextSegment(posIndex);
                }
            }
        }

        private void NextSegment(int index)
        {
            var t0 = path[index];
            var t1 = path[index + 1];
            stepSize = speed / (t0.IsDiagonalTo(t1) ? ROOT_TWO : 1f);
            start = t0.WorldPosition.Vector2XY();
            end = t1.WorldPosition.Vector2XY();
            progress = 0;
        }
    }
}

