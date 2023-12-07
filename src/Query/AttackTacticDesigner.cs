using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using System;

namespace FinalEmblem.QueryModel
{
    public partial class AttackTacticDesigner : Node, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        private List<Tile> inRange;
        private GameMap map;

        private const int TILE_ALT_ID = 1;

        public void Initialize(Unit attacker, GameMap map)
        {
            this.map = map;
            inRange = NavService.FindTilesInRange(1, attacker.Tile, includeStart: false);
            map.HighlightGameTiles(inRange, TILE_ALT_ID);
        }

        public virtual void SetSelectedTile(Tile tile) 
        {
            // hard code attack enemy faction 
            if (inRange.Contains(tile) && tile.Unit?.Faction != Faction.Player)
            {
                var action = new AttackAction(tile.Unit);
                OnActionBuilt?.Invoke(action);
                map.ClearTileHighlights();
                QueueFree();
            }
        }
    }
}

