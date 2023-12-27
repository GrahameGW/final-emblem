using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class AnimationController : Node
    {
        [Export] PackedScene attackDisplay;

        private Game level;
        private ActionAnimator animator;
        private readonly List<Node> animations = new();

        public void Initialize(Game level)
        {
            this.level = level;
            level.OnTurnEnded += TurnEndedHandler;
        }

        public override void _ExitTree()
        {
            level.OnTurnEnded -= TurnEndedHandler;
        }

        public void LoadActionAnim(IAction toAnimate)
        {
            animator = toAnimate.Type switch
            {
                ActionType.Move => new MoveActionAnimator(toAnimate as MoveAction),
                ActionType.Wait => new WaitActionAnimator(toAnimate.Actor),
                ActionType.Attack => InstantiateAttackAnim(toAnimate as AttackAction),
                ActionType.Die => new DeathActionAnimator(toAnimate.Actor),
                ActionType.Collide => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException(toAnimate.Type.ToString()),
            };
        }

        private AttackActionAnimator InstantiateAttackAnim(AttackAction action)
        {
            var display = attackDisplay.Instantiate<AttackAnimationDisplay>();
            AddChild(display);
            animations.Add(display);
            return new AttackActionAnimator(action, display);
        }

        public async Task PlayActionAnim(IActionResult result)
        {
            AddChild(animator);
            if (animator is IAwaitActionResult awating)
            {
                awating.ReceiveActionResult(result);
            }
            await ToSignal(animator, ActionAnimator.AnimCompleteSignal);
        }

        public void ClearActionAnim()
        {
            animator.QueueFree();
            animator = null;
            foreach (var anim in animations)
            {
                anim.QueueFree();
            }
            animations.Clear();
        }

        private void TurnEndedHandler()
        {
            for (int i = 0; i < level.Units.Count; i++)
            {
                level.Units[i].ToggleActedMaterial(false);
            }
        }


    }
}

