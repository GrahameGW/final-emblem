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

        public List<UnitAction> Actions { get; set; }

        private Queue<IAction> actionQueue = new();
        private Tile _tile;
        private bool _hasActed;
        private int _move;
        private int _hp;

        public event Action<int> OnUnitMoveChanged;
        public event Action<int> OnUnitHpChanged;
        public event Action<bool> OnUnitHasActedChanged;
        public event Action<Unit> OnUnitDied;
        public event Action<Tile> OnTileChanged;

        public List<UnitAction> GetAvailableActions()
        {
            if (HasActed)
            {
                return null;
            }

            if (HasMoved)
            {
                return Actions.Where(a => a != UnitAction.Move).ToList();
            }

            return Actions;
        }

        public void ClearActions()
        {
            actionQueue.Clear();
        }
        public void EnqueueAction(IAction action)
        {
            actionQueue.Enqueue(action);
        }
        public IAction DequeueAction()
        {
            if (actionQueue.Count == 0)
            {
                return null;
            }
            return actionQueue.Dequeue();
        }
        public IAction PeekAction()
        {
            if (actionQueue.Count > 0)
            {
                return actionQueue.Peek();
            }
            return null;
        }
        public void DoNextAction()
        {
            var action = DequeueAction();
            action?.Execute(this);
            // combatservice do trigger stuff
            if (actionQueue.Count == 0)
            {
                if (action is MoveActionOld)
                {
                    HasMoved = true;
                }
                else
                {
                    HasActed = true;
                }
            }
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
