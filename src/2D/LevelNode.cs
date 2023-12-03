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
            grid = gameMap.GenerateGridFromMap();
            factions = GenerateFactionsFromMap(gameMap);
            level = new Level(grid, factions.SelectMany(f => f.Units).ToList());

            player = GetNode<PlayerController>("Player");
            var actionList = GetNode<ActionList>("HUD/ActionList");
            var playerUnits = factions.FirstOrDefault(f => f.Units[0].Faction == Faction.Player);
            player.Initialize(gameMap, playerUnits);
            actionList.Initialize(player);

            NavService.SetGridInstance(grid);
            CombatService.SetGridInstance(grid);

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

            player?.UnitNodes.AddRange(playerUnits);
            enemy?.UnitNodes.AddRange(enemyUnits);
            other?.UnitNodes.AddRange(otherUnits);

            // TODO: Add to faction controllers

            return new List<UnitGroup> { player, enemy, other };
        }
    }
}

