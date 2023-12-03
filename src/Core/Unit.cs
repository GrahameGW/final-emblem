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
        public int Move { get; set; }
        public Faction Faction { get; set; }
        public bool HasActed { get; set; }
        public bool HasMoved { get; set; }

        public List<UnitAction> Actions { get; set; }

        private Queue<IAction> actionQueue = new();
        private Tile _tile;

        public event Action<IAction> OnActionStarted;
        public event Action<IAction> OnActionProcessed;
        public event Action<IAction> OnActionFinished;

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

        public void StartActionPlayback(IAction action)
        {
            OnActionStarted?.Invoke(action);
        }

        public void ContinueActionPlayback(IAction action)
        {
            OnActionProcessed?.Invoke(action);
        }

        public void FinishActionPlayback(IAction action)
        {
            OnActionFinished?.Invoke(action);
        }
    }
}
