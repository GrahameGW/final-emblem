﻿using Godot;
using System.Collections.Generic;
using System;

namespace FinalEmblem.Core
{
    public partial class AttackTacticDesigner : Node, ITacticDesigner
    {
        public Action<List<IUnitAction>> OnActionBuilt { get; set; }

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

        public void SetSelectedTile(Tile tile) 
        {
            // hard code attack enemy faction 
            if (inRange.Contains(tile) && tile.Unit?.Faction != Faction.Player)
            {
                var action = new AttackAction(attacker, tile.Unit);
                var actuals = CombatService.CalculateAttackImplications(attacker, action);
                OnActionBuilt?.Invoke(actuals);
                map.ClearTileHighlights();
                QueueFree();
            }
        }
    }
}

