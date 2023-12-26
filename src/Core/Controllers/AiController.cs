using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class AiController : ControllerBase
    {
        public override string DebugName => "AiController";

        private Game level;
        
        public void Initialize(Game level, Faction faction)
        {
            this.level = level;
            Faction = faction;
        }
        
        public override void _EnterTree()
        {
            GD.Print("AI Turn");
            level.StartTurn(Faction);
            var units = level.ActingUnits;
            var enemies = level.Units.Where(u => u.Faction == Faction.Player).ToList();
            Act(units, enemies);
        }

        private async void Act(List<Unit> units, List<Unit> enemies)
        {
            foreach (var unit in units) 
            {
                if (level.TestWinConditions())
                {
                    return;
                };

                if (await TryAttackNeighbor(unit, enemies))
                {
                    continue;
                }
                
                var tilesInRange = NavService.FindTilesInRange(unit.Move, unit.Tile);
                var possibleDestinations = enemies.SelectMany(e => e.Tile.GetNeighbors())
                                                  .Where(s => tilesInRange.Contains(s))
                                                  .ToList();

                if (possibleDestinations.Count == 0)
                {
                    var path = NavService.ShortestPathToCollection(unit.Tile, enemies.Select(e => e.Tile).ToList());
                    var move = new MoveAction(unit, path.PathTilesInRange(unit.Move));
                    var actuals = CombatService.CalculateActionImplications(unit, move);
                    await ExecuteActions(actuals);
                    unit.HasMoved = true;
                    var wait = new WaitAction() { Actor = unit };
                    actuals = CombatService.CalculateActionImplications(unit, wait);
                    await ExecuteActions(actuals);
                    unit.HasActed = true;
                }
                else
                {
                    var move = new MoveAction(unit, NavService.ShortestPathToCollection(unit.Tile, possibleDestinations));
                    var actuals = CombatService.CalculateActionImplications(unit, move);
                    await ExecuteActions(actuals);
                    unit.HasMoved = true;
                    var target = possibleDestinations[0].GetNeighbors().First(t => t.Unit != null && t.Unit.Faction != Faction);
                    var attack = new AttackAction(unit, target.Unit);
                    actuals = CombatService.CalculateActionImplications(unit, attack);
                    await ExecuteActions(actuals);
                    unit.HasActed = true;
                }
            }

            level.EndTurn();
            ReleaseControl();
        }

        private Task ExecuteActions(List<IAction> actions)
        {
            var tcs = new TaskCompletionSource();
            level.ExecuteActionQueue(actions, () => tcs.SetResult());
            return tcs.Task;
        }

        private async Task<bool> TryAttackNeighbor(Unit unit, List<Unit> enemies)
        {
            foreach (var tile in unit.Tile.GetNeighbors())
            {
                if (enemies.Contains(tile.Unit))
                {
                    var attack = new AttackAction(unit, tile.Unit);
                    var actuals = CombatService.CalculateActionImplications(unit, attack);
                    await ExecuteActions(actuals);
                    unit.HasActed = true;
                    return true;
                }
            }

            return false;
        }
    }
}

