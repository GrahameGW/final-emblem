using Godot;
using System.Collections.Generic;
using System;

namespace FinalEmblem.Core
{
    public partial class SpeakActionDesigner : Node, ITacticDesigner
    {
        public Action<List<IUnitAction>> OnActionBuilt { get; set; }
        public Unit Unit { get; private set; }
        private GameMap map;
        private List<Tile> inRange;

        private const int TILE_ALT_ID = 1;
        
        public void Initialize(Unit unit, GameMap map)
        {
            this.map = map;
            this.Unit = unit;
            inRange = NavService.FindTilesInRange(1, unit.Tile, includeStart: false);
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
            if (tile.Feature == null || tile.Feature.InteractMode == FeatureInteraction.None)
            {
                return;
            }
            if (tile.Feature.Interaction == UnitAction.Speak)
            {
                var timeline = tile.Feature.Data;
                var action = new SpeakAction(Unit, timeline, tile);
                var actuals = CombatService.CalculateSpeakImplications(action);
                OnActionBuilt?.Invoke(actuals);
                map.ClearTileHighlights();
                QueueFree();
            }
        }
    }
}

