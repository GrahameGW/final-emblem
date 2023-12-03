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
        private Tile _tile;

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
    }
}
