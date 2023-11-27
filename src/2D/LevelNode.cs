using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System.Linq;

namespace FinalEmblem.Godot2D
{
    public partial class LevelNode : Node
    {
        private GameMap gameMap;
        private Grid grid;
        private List<Faction> factions = new();
        private Level level;

        public override void _Ready()
        {
            gameMap = GetNode<GameMap>("GameMap");
            grid = gameMap.GenerateGridFromMap();
            factions = GenerateFactionsFromMap(gameMap);
            level = new Level(grid, factions);
            NavService.SetGridInstance(grid);
        }

        private List<Faction> GenerateFactionsFromMap(GameMap map)
        {
            var player = new Faction();
            var enemy = new Faction();
            var other = new Faction();

            var units = this.FindNodesOfType(new List<UnitNode>());

            var playerUnits = units.Where(u => u.Faction == FactionName.Player).ToList();
            var enemyUnits = units.Where(u => u.Faction == FactionName.Enemy).ToList();
            var otherUnits = units.Where(u => u.Faction == FactionName.Other).ToList();

            void AssignUnitTile(List<UnitNode> units)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    var tile = map.GetGridTile(units[i].GlobalPosition);
                    if (tile.Unit != null)
                    {
                        GD.PrintErr($"Multiple units have been loaded to tile {tile.Coordinates}: {tile.Unit}, {units[i]}");
                    }
                    units[i].Unit.Tile = tile;
                }
            }

            AssignUnitTile(playerUnits);
            AssignUnitTile(enemyUnits);
            AssignUnitTile(otherUnits);

            player.Units.AddRange(playerUnits.Select(u => u.Unit));
            enemy.Units.AddRange(enemyUnits.Select(u => u.Unit));
            other.Units.AddRange(otherUnits.Select(u => u.Unit));

            return new List<Faction> { player, enemy, other };
        }
    }

    public partial class UnitManager : Node
    {
        [Export] PackedScene unitPrefab;

        private List<UnitNode> units = new();

        public void AddUnitsToGrid(List<Unit> toAdd)
        {
            for (int i = 0; i < toAdd.Count; i++)
            {
               // if (units.Select(u => u.unit)
            }
        }
    }
}

