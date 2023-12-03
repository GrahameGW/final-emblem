using Godot;
using System.Collections;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class MoveAction : IAction
    {
        private readonly List<Tile> path;

        public MoveAction(List<Tile> path)
        {
            this.path = path;
        }

        public IEnumerator Execute(Unit unit)
        {
            if (!path[0].IsNeighborOf(unit.Tile)) 
            {
                GD.PushWarning($"The move path {unit} is attempting does not start adjacent to its current tile." +
                    $"\nCurrent Tile: {unit.Tile}. First path tile: {path[0]}");
            }

            for (int i = 0; i < path.Count; i++)
            {
                unit.Tile = path[i];
                yield return null;
            }
        }
    }
}

