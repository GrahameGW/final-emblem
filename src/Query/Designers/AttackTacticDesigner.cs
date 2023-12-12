using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using System;
using FinalEmblem.src.Query.Designers;

namespace FinalEmblem.QueryModel
{
    public partial class AttackTacticDesigner : Node, ITacticDesigner
    {
        public Action<IAction> OnActionBuilt { get; set; }

        private Unit attacker;
        private List<Tile> inRange;
        private GameMap map;

        private const int TILE_ALT_ID = 1;

        public void Initialize(Unit attacker, GameMap map)
        {
            this.map = map;
            this.attacker = attacker;
            inRange = NavService.FindTilesInRange(1, attacker.Tile, includeStart: false);
        }

        public override void _EnterTree()
        {
            map.HighlightGameTiles(inRange, TILE_ALT_ID);
        }

        public override void _ExitTree()
        {
            map.ClearTileHighlights();
        }

        public virtual void SetSelectedTile(Tile tile) 
        {
            // hard code attack enemy faction 
            if (inRange.Contains(tile) && tile.Unit?.Faction != Faction.Player)
            {
                var action = new AttackAction(attacker, tile.Unit);
                OnActionBuilt?.Invoke(action);
                map.ClearTileHighlights();
                QueueFree();
            }
        }
    }
}

