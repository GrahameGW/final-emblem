using Godot;
using FinalEmblem.Core;
using System.Collections.Generic;
using TiercelFoundry.GDUtils;
using System.Linq;
using FinalEmblem.src.Core.Services;

namespace FinalEmblem.Godot2D
{
    public partial class LevelNode : Node
    {
        private GameMap gameMap;
        private Grid grid;
        private List<UnitGroup> factions = new();
        private Level level;
        private PlayerController player;
        private UIController ui;

        public override void _Ready()
        {
            gameMap = GetNode<GameMap>("GameMap");
            grid = gameMap.GenerateGridFromMap();
            factions = GenerateFactionsFromMap(gameMap);
            level = new Level(grid, factions.Select(f => f.Units).ToList());

            player = GetNode<PlayerController>("Player");
            var playerUnits = factions.FirstOrDefault(f => f.Units[0].Faction == Faction.Player);
            player.Initialize(gameMap, playerUnits, level);

            ui = GetNode<UIController>("HUD");
            ui.Initialize(level, player);

            NavService.SetGridInstance(grid);
            CombatService.SetGridInstance(grid);

            level.StartTurn(Faction.Player, factions[0].Units);
        }

        private List<UnitGroup> GenerateFactionsFromMap(GameMap map)
        {
            var player = new UnitGroup(Faction.Player);
            var enemy = new UnitGroup(Faction.Enemy);
            var other = new UnitGroup(Faction.Other);

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

            var group = new List<UnitGroup>();

            if (playerUnits.Count > 0)
            {
                player?.UnitNodes.AddRange(playerUnits);
                group.Add(player);
            }
            if (enemyUnits.Count > 0)
            {
                enemy?.UnitNodes.AddRange(enemyUnits);
                group.Add(enemy);
            }
            if (otherUnits.Count > 0)
            {
                other?.UnitNodes.AddRange(otherUnits);
                group.Add(other);
            }

            // TODO: Add to faction controllers
            return group;
        }

        public void EndTurn()
        {
            level.EndTurn();
        }
    }
}

