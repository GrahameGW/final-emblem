using Godot;
using FinalEmblem.Core;
using Godot.Collections;
using System.Linq;
using System.Collections;
using TiercelFoundry.GDUtils;
using System;

namespace FinalEmblem.Godot2D
{
    [GlobalClass]
    public partial class UnitNode : Node2D
    {
        public Unit Unit { get; private set; }

        [ExportGroup("Unit Stats")]
        [Export] public int Move { get; private set; }
        [Export] public Faction Faction { get; private set; }
        [Export] public Array<UnitAction> Actions { get; private set; }

        [ExportGroup("Playback")]
        [Export] float travelSpeed;

        private ActionPlayback currentPlayback;
        private IAction currentAction;
        private bool isPlayingActions;

        private UnitSprite sprite;

        public event Action OnActionPlaybackCompleted;


        public void Initialize()
        {
            Unit = new Unit()
            {
                Move = Move,
                Faction = Faction,
                Actions = Actions.ToList()
            };

            isPlayingActions = false;
            sprite = this.FindChildOfType<UnitSprite>();
            Unit.OnUnitHasActedChanged += _ => UnitHasActedHandler();
        }

        public override void _ExitTree()
        {
            Unit.OnUnitHasActedChanged -= _ => UnitHasActedHandler();
        }

        public override void _Process(double delta)
        {
            currentPlayback?.Update(delta);
        }

        public void PlayNextAction()
        {
            currentAction = Unit.DequeueAction();

            if (currentAction == null)
            {
                isPlayingActions = false;
                currentPlayback = null;
                OnActionPlaybackCompleted?.Invoke();
                return;
            }

            CombatService.TryExecution(Unit, currentAction);
            currentPlayback = InitializePlayback(currentAction);
            isPlayingActions = true;

            if (currentAction is MoveAction)
            {
                Unit.HasMoved = true;
            }
            else
            {
                Unit.HasActed = true;
            }
        }

        private ActionPlayback InitializePlayback(IAction action)
        {
            if (action is MoveAction)
            {
                var move = action as MoveAction;
                return new MoveActionPlayback(this, move.from, move.to, travelSpeed);
            }

            if (action is WaitAction)
            {
                return new WaitActionPlayback(this);
            }

            throw new NotImplementedException();
        }

        private void UnitHasActedHandler()
        {
            sprite.ToggleMaterial(Unit.HasActed);
        }
    }
}

