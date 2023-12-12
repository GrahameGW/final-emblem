using Godot;
using FinalEmblem.Core;
using Godot.Collections;
using System.Linq;
using TiercelFoundry.GDUtils;

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
        [Export] public Array<UnitTactic> Actions { get; private set; }

        [ExportGroup("Playback")]
        [Export] public float TravelSpeed { get; private set; }

        [ExportGroup("Materials")]
        [Export] Material defaultMaterial;
        [Export] Material unitActedMaterial;

        private Sprite2D sprite;

        public override void _EnterTree()
        {
            sprite = this.FindChildOfType<Sprite2D>();
        }

        public Unit GenerateUnitFromToken()
        {
            Unit = new Unit()
            {
                Move = Move,
                MaxHP = HP,
                HP = HP,
                Attack = Attack,
                Faction = Faction,
                Actions = Actions.ToList()
            };

            return Unit;
        }

        public override void _ExitTree()
        {
            Unit.OnUnitHpChanged -= hp => { HP = hp; };
        }

        public void ToggleActedMaterial()
        {
            sprite.Material = Unit.HasActed ? unitActedMaterial : defaultMaterial;
        }
    }
}

