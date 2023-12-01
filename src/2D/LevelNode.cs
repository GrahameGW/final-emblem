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
        private List<UnitGroup> factions = new();
        private Level level;
        private PlayerController player;

        public override void _Ready()
        {
            gameMap = GetNode<GameMap>("GameMap");
            player = GetNode<PlayerController>("Player");
            player.Initialize(gameMap);
            grid = gameMap.GenerateGridFromMap();
            factions = GenerateFactionsFromMap(gameMap);
            level = new Level(grid, factions.SelectMany(f => f.Units).ToList());
            NavService.SetGridInstance(grid);

            level.StartTurn(Faction.Player);
        }

        private List<UnitGroup> GenerateFactionsFromMap(GameMap map)
        {
            var player = new UnitGroup();
            var enemy = new UnitGroup();
            var other = new UnitGroup();

            var units = this.FindNodesOfType(new List<UnitNode>());
            for (int i = 0; i < units.Count; i++)
            {
                units[i].Initialize();
            }

            var playerUnits = units.Where(u => u.Faction == Faction.Player).ToList();
            var enemyUnits = units.Where(u => u.Faction == Faction.Enemy).ToList();
            var otherUnits = units.Where(u => u.Faction == Faction.Other).ToList();

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
                    units[i].GlobalPosition = new Vector2(tile.WorldPosition.X, tile.WorldPosition.Y);
                }
            }

            AssignUnitTile(playerUnits);
            AssignUnitTile(enemyUnits);
            AssignUnitTile(otherUnits);

            player.Units.AddRange(playerUnits.Select(u => u.Unit));
            enemy.Units.AddRange(enemyUnits.Select(u => u.Unit));
            other.Units.AddRange(otherUnits.Select(u => u.Unit));

            // TODO: Add to faction controllers

            return new List<UnitGroup> { player, enemy, other };
        }
    }
}

