using FinalEmblem.Core;
using Godot;
using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.QueryModel
{
    public partial class AttackActionAnimator : ActionAnimator
    {
        private UnitToken actor;
        private List<Unit> targets;

        public AttackActionAnimator(UnitToken actor, List<Tile> affected)
        {
            this.actor = actor;
            targets = affected.Select(u => u.Unit).ToList();
        }

        public override async void StartAnimation()
        {
            GD.Print($"{actor.Unit} did an attack! Hurt {targets[0]} and {targets.Count - 1} others");
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            EmitSignal(AnimCompleteSignal);
        }
    }
}

