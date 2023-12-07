using Godot;
using FinalEmblem.Core;
using Godot.Collections;
using System.Linq;
using System;

namespace FinalEmblem.QueryModel
{
    [GlobalClass]
    public partial class UnitToken : Node2D
    {
        public Unit Unit { get; private set; }

        [ExportGroup("Unit Stats")]
        [Export] public int Move { get; private set; }
        [Export] public int HP { get; private set; }
        [Export] public int Attack { get; private set; }
        [Export] public Faction Faction { get; private set; }
        [Export] public Array<UnitAction> Actions { get; private set; }

        [ExportGroup("Playback")]
        [Export] float travelSpeed;

        [ExportGroup("Materials")]
        [Export] Material defaultMaterial;
        [Export] Material unitActedMaterial;

        //private ActionPlayback currentPlayback;
        private IAction currentAction;
        private bool isPlayingActions = false;

        public event Action OnActionPlaybackCompleted;


        public Unit GenerateUnitFromToken()
        {
            return new Unit()
            {
                Move = Move,
                MaxHP = HP,
                HP = HP,
                Attack = Attack,
                Faction = Faction,
                Actions = Actions.ToList()
            };
        }

        public void SetUnit(Unit unit)
        {
            if (unit == null)
            {
                GD.PushWarning($"Tried to set a null value for Unit on token {Name}. This is not allowed.");
                return;
            }

            Unit = unit;
            Unit.OnUnitHasActedChanged += _ => UnitHasActedHandler();
            Unit.OnUnitHpChanged += hp => { HP = hp; };
        }

        public override void _ExitTree()
        {
            Unit.OnUnitHasActedChanged -= _ => UnitHasActedHandler();
            Unit.OnUnitHpChanged -= hp => { HP = hp; };
        }

        public override void _Process(double delta)
        {
            // currentPlayback?.Update(delta);
        }

        public void PlayNextAction()
        {
            currentAction = Unit.DequeueAction();

            if (currentAction == null)
            {
                isPlayingActions = false;
              //  currentPlayback = null;
                OnActionPlaybackCompleted?.Invoke();
                return;
            }

            CombatService.TryExecution(Unit, currentAction);
           // currentPlayback = InitializePlayback(currentAction);
            isPlayingActions = true;

            if (currentAction is MoveActionOld)
            {
                Unit.HasMoved = true;
            }
            else
            {
                Unit.HasActed = true;
            }
        }

        /*
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
            if (action is AttackAction)
            {
                // return new AttackActionPlayback();
                return new WaitActionPlayback(this);
            }

            throw new NotImplementedException();
        }
        */
        private void UnitHasActedHandler()
        {
            // Material = Unit.HasActed ? unitActedMaterial : defaultMaterial;
        }
    }
}

