using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using TiercelFoundry.GDUtils;

namespace FinalEmblem.Core
{
    public partial class Unit : Node2D
    {
        public Tile Tile
        {
            get => _tile;
            set
            {
                if (_tile != null)
                {
                    _tile.Unit = null;
                }
                _tile = value;
                _tile.Unit = this;
                OnTileChanged?.Invoke(value);
            }
        }
        public Compass Facing { get; set; } = Compass.S;
        public List<UnitAction> Actions { get; private set; }
        public bool HasActed 
        { 
            get => _hasActed; 
            set
            {
                _hasActed = value;
                OnUnitHasActedChanged?.Invoke(value);
            }
        }
        public bool HasMoved { get; set; }

        [ExportGroup("Unit Stats")]
        [Export] public int Move
        {
            get => _move;
            set
            {
                _move = value;
                OnUnitMoveChanged?.Invoke(value);
            }
        }
        [Export] public int HP { 
            get => _hp;
            set
            {
                _hp = value;
                OnUnitHpChanged?.Invoke(value);
            }
        }
        [Export] public int MaxHP { get; set; }
        [Export] public int Strength { get; set; }
        [Export] public Faction Faction { get; set; }
        [Export] public Weapon Weapon { get; set; }
        [Export] Array<UnitAction> actions;

        [ExportGroup("Playback")]
        [Export] public float TravelSpeed { get; private set; }
        [Export] public AudioStreamMP3 FootstepsClip { get; private set; }

        [ExportGroup("Materials")]
        [Export] Material defaultMaterial;
        [Export] Material unitActedMaterial;

        private Tile _tile;
        private bool _hasActed;
        private int _move;
        private int _hp;
        private Sprite2D sprite;
        private AnimationPlayer animator;

        public event Action<int> OnUnitMoveChanged;
        public event Action<int> OnUnitHpChanged;
        public event Action<bool> OnUnitHasActedChanged;
        public event Action OnUnitDied;
        public event Action<Tile> OnTileChanged;


        public override void _Ready()
        {
            Actions = actions.ToList();
            animator = GetNode<AnimationPlayer>("AnimationPlayer");
        }

        public override void _EnterTree()
        {
            sprite = this.FindChildOfType<Sprite2D>();
        }

        public override void _ExitTree()
        {
            Tile.Unit = null;
        }

        public List<UnitAction> GetAvailableActions()
        {
            if (HasActed) { return null; }

            var actions = new List<UnitAction>();
            actions.AddRange(HasMoved ? Actions.Where(a => a != UnitAction.Move) : Actions);
            actions.AddRange(Tile.Interactions());

            return actions;
        }

        public void Damage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                OnUnitDied?.Invoke();
            }
        }

        public void ToggleActedMaterial(bool hasActed)
        {
            sprite.Material = hasActed ? unitActedMaterial : defaultMaterial;
        }

        public void SetIdleAnimation()
        {
            var anim = Facing switch
            {
                Compass.N => "idle_up",
                Compass.E => "idle_right",
                Compass.S => "idle_down",
                Compass.W => "idle_left",
                _ => throw new NotImplementedException()
            };
            animator.Play(anim);
        }
        public void SetIdleAnimation(Compass direction)
        {
            var anim = direction switch
            {
                Compass.N => "idle_up",
                Compass.E => "idle_right",
                Compass.S => "idle_down",
                Compass.W => "idle_left",
                _ => throw new NotImplementedException()
            };
            animator.Play(anim);
            
        }
    }
}
