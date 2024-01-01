
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using Godot;
using System;

namespace FinalEmblem.Core
{
    public partial class MoveActionAnimator : OneShotAnimation
    {
        private Unit actor;
        private List<Tile> path;
        private float speed;
        private float stepSize;
        private int posIndex;
        private float progress;
        private Vector2 start;
        private Vector2 end;
        private Compass facing;

        private AnimationPlayer animator;
        private AudioStreamPlayer2D audio;

        private const float ROOT_TWO = 1.41421356f;

        public MoveActionAnimator(MoveAction action)
        {
            path = action.Path;
            actor = action.Unit;
            speed = action.Unit.TravelSpeed;
            progress = 0f;
            posIndex = 0;

            animator = actor.GetNode<AnimationPlayer>("AnimationPlayer");
            audio = actor.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        }

        public override void _EnterTree()
        {
            if (path.Count < 2)
            {
                EmitSignal(AnimCompleteSignal);
                return;
            }

            NextSegment(posIndex);
            audio.Stream = actor.FootstepsClip;
            audio.Play();
        }

        public override void _Process(double deltaTime)
        {
            progress += stepSize * (float)deltaTime;
            actor.Position = start.Lerp(end, progress);

            if (progress >= 1)
            {
                actor.Position = end;
                posIndex += 1;
                if (posIndex == path.Count - 1)
                {
                    actor.SetIdleAnimation(facing);
                    audio.Stop();
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
            facing = t0.DirectionToApproxDiagonals(t1, true); 
            string anim = facing switch
            {
                Compass.N => "move_up",
                Compass.E => "move_right",
                Compass.S => "move_down",
                Compass.W => "move_left",
                _ => throw new NotImplementedException()
            };

            animator.Play(anim);
        }

    }
}

