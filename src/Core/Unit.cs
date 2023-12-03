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
                _tile = value;
                _tile.Unit = this;
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
        public int Move { get; set; }
        public Faction Faction { get; set; }

        public bool HasMoved { get; set; }

        public List<UnitAction> Actions { get; set; }

        private Queue<IAction> actionQueue = new();
        private Tile _tile;
        private bool _hasActed;

        public event Action<bool> OnUnitHasActedChanged;


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
    }
}
