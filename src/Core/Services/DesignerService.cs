using Godot;

namespace FinalEmblem.Core
{
    public static class DesignerService
    {
        private const string MOVE_DESIGNER = "res://scenes/MoveTacticDesigner.tscn";

        private static PackedScene packedMoveDesigner;

        public static void Initialize()
        {
            packedMoveDesigner = GD.Load<PackedScene>(MOVE_DESIGNER);
        }

        public static MoveTacticDesigner GetMoveDesigner(Tile start, GameMap map)
        {
            var designer = packedMoveDesigner.Instantiate<MoveTacticDesigner>();
            designer.Initialize(start, map);
            return designer;
        }

        public static AttackTacticDesigner GetAttackTacticDesigner(Unit attacker, GameMap map)
        {
            var designer = new AttackTacticDesigner();
            designer.Initialize(attacker, map);
            return designer;
        }

        public static WaitTacticDesigner GetWaitTacticDesigner(Unit unit)
        {
            var designer = new WaitTacticDesigner(unit);
            return designer;
        }
    }
}
