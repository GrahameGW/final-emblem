using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalEmblem.Core
{
    public class Unit
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
        public bool HasActed 
        { 
            get => _hasActed; 
            set
            {
                _hasActed = value;
                OnUnitHasActedChanged?.Invoke(value);
            }
        }

        public int Move
        {
            get => _move;
            set
            {
                _move = value;
                OnUnitMoveChanged?.Invoke(value);
            }
        }
        public int HP { 
            get => _hp;
            set
            {
                _hp = value;
                OnUnitHpChanged?.Invoke(value);
            }
        }
        public int MaxHP { get; set; }
        public int Attack { get; set; }
        public Faction Faction { get; set; }

        public bool HasMoved { get; set; }

        public List<UnitTactic> Actions { get; set; }

        private Tile _tile;
        private bool _hasActed;
        private int _move;
        private int _hp;

        public event Action<int> OnUnitMoveChanged;
        public event Action<int> OnUnitHpChanged;
        public event Action<bool> OnUnitHasActedChanged;
        public event Action<Unit> OnUnitDied;
        public event Action<Tile> OnTileChanged;

        public List<UnitTactic> GetAvailableActions()
        {
            if (HasActed)
            {
                return null;
            }

            if (HasMoved)
            {
                return Actions.Where(a => a != UnitTactic.Move).ToList();
            }

            return Actions;
        }

        public void Damage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
            }
        }
    }
}
